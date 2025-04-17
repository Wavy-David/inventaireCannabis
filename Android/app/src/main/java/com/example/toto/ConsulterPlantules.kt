package com.example.toto

import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.toto.databinding.ConsulterPlantesBinding
import java.text.SimpleDateFormat
import java.util.Date
import java.util.Locale

class ConsulterPlantules : AppCompatActivity() {

    private lateinit var binding: ConsulterPlantesBinding
    private lateinit var dbHelper: DataBaseHelper

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ConsulterPlantesBinding.inflate(layoutInflater)
        setContentView(binding.root)

        dbHelper = DataBaseHelper(this)
        binding.recyclerViewPlants.layoutManager = LinearLayoutManager(this)

        // Afficher toutes les plantes dès l'ouverture
        displayAllPlants()

        binding.buttonSearch.setOnClickListener {
            val id = binding.searchId.text.toString().trim()
            if (id.isEmpty()) {
                Toast.makeText(this, "Veuillez entrer un ID", Toast.LENGTH_SHORT).show()
            } else {
                displayPlantuleById(id)

                // Enregistrer l'action de consultation dans l'historique
                enregistrerHistoriqueConsultation(id)
            }
        }
    }

    private fun displayAllPlants() {
        val allPlants = dbHelper.getAllPlantules()
        if (allPlants.isNotEmpty()) {
            binding.recyclerViewPlants.adapter = PlantuleAdapter(allPlants)
            binding.statusMessage.text = "Toutes les plantes affichées."
        } else {
            binding.recyclerViewPlants.adapter = null
            binding.statusMessage.text = "Aucune plante disponible."
        }
    }

    private fun displayPlantuleById(id: String) {
        val plant = dbHelper.getPlantuleById(id)

        if (plant != null) {
            val adapter = PlantuleAdapter(listOf(plant))
            binding.recyclerViewPlants.adapter = adapter
            binding.statusMessage.text = "Plante trouvée."
        } else {
            binding.statusMessage.text = "Aucune plante trouvée avec l'ID \"$id\"."
            binding.recyclerViewPlants.adapter = null
        }
    }

    private fun enregistrerHistoriqueConsultation(idPlante: String) {
        val db = dbHelper.writableDatabase
        val values = android.content.ContentValues()

        values.put("idPlante", idPlante.uppercase())
        values.put("action", "Consulter")
        values.put("champ", "__")
        values.put("ancienneValeur", "__")
        values.put("nouvelleValeur", "__")

        val date = SimpleDateFormat("yyyy-MM-dd", Locale.getDefault()).format(Date())
        values.put("Date", date)

        db.insert("HistoriquePlante", null, values)
        db.close()
    }
}
