package com.example.toto

import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.toto.databinding.ArchivesBinding
import java.text.SimpleDateFormat
import java.util.*

class Archives : AppCompatActivity() {

    private lateinit var binding: ArchivesBinding
    private lateinit var dbHelper: DataBaseHelper
    private var currentPlantule: PlantuleModel? = null

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ArchivesBinding.inflate(layoutInflater)
        setContentView(binding.root)

        dbHelper = DataBaseHelper(this)
        binding.recyclerViewPlants.layoutManager = LinearLayoutManager(this)

        afficherToutesLesPlantes()
        binding.buttonSupprimerBottom.isEnabled = false

        binding.buttonSearch.setOnClickListener {
            val id = binding.searchId.text.toString().trim()
            if (id.isNotEmpty()) {
                afficherUneArchiveParId(id)
            } else {
                Toast.makeText(this, "Veuillez entrer un ID.", Toast.LENGTH_SHORT).show()
            }
        }

        binding.buttonSupprimerBottom.setOnClickListener {
            currentPlantule?.let { plant ->
                val successArchive = dbHelper.addPlantuleToArchive(plant)
                val successDelete = dbHelper.deletePlantuleById(plant.idPlante)

                if (successArchive && successDelete) {
                    // Enregistrer l'action dans HistoriquePlante
                    enregistrerHistoriqueArchivage(plant.idPlante)
                    Toast.makeText(this, "Plante archivée avec succès.", Toast.LENGTH_SHORT).show()
                    currentPlantule = null
                    afficherToutesLesPlantes()
                    binding.buttonSupprimerBottom.isEnabled = false
                } else {
                    Toast.makeText(this, "Erreur lors de l'archivage.", Toast.LENGTH_SHORT).show()
                }
            }
        }
    }

    private fun afficherToutesLesPlantes() {
        val allPlants = dbHelper.getAllPlantules()
        binding.recyclerViewPlants.adapter = PlantuleAdapter(allPlants)
        binding.statusMessage.text = "${allPlants.size} plantes trouvées."
    }

    private fun afficherUneArchiveParId(id: String) {
        val plant = dbHelper.getPlantuleById(id)
        if (plant != null) {
            currentPlantule = plant
            binding.recyclerViewPlants.adapter = PlantuleAdapter(listOf(plant))
            binding.statusMessage.text = "Plante trouvée pour ID : $id"
            binding.buttonSupprimerBottom.isEnabled = true
        } else {
            binding.recyclerViewPlants.adapter = null
            binding.statusMessage.text = "Aucune plante trouvée pour ID : $id"
            binding.buttonSupprimerBottom.isEnabled = false
        }
    }

    private fun enregistrerHistoriqueArchivage(idPlante: String) {
        val db = dbHelper.writableDatabase
        val cv = android.content.ContentValues()

        val date = SimpleDateFormat("yyyy-MM-dd", Locale.getDefault()).format(Date())

        cv.put("idPlante", idPlante)
        cv.put("action", "archiver")
        cv.put("Date", date)
        cv.put("champ", "__")
        cv.put("ancienneValeur", "__")
        cv.put("nouvelleValeur", "__")

        db.insert("HistoriquePlante", null, cv)
        db.close()
    }
}
