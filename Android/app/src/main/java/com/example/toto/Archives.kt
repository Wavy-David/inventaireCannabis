package com.example.toto

import android.content.Intent
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.toto.databinding.ArchivesBinding

class Archives : AppCompatActivity() {

    private lateinit var binding: ArchivesBinding
    private var lastFoundPlant: PlantuleModel? = null  // holds the found plant for deletion

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ArchivesBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.recyclerViewPlants.layoutManager = LinearLayoutManager(this)
        binding.buttonSupprimerBottom.isEnabled = false // disabled initially

        // Handle search click
        binding.buttonSearch.setOnClickListener {
            val id = binding.searchId.text.toString().trim()
            if (id.isEmpty()) {
                Toast.makeText(this, "Veuillez entrer un ID", Toast.LENGTH_SHORT).show()
                return@setOnClickListener
            }

            val db = DataBaseHelper(this)
            val plantule = db.getPlantuleById(id.uppercase())

            if (plantule != null) {
                lastFoundPlant = plantule
                binding.recyclerViewPlants.adapter = PlantuleAdapter(listOf(plantule))
                binding.statusMessage.text = "Plantule trouvée."
                binding.buttonSupprimerBottom.isEnabled = true
            } else {
                lastFoundPlant = null
                binding.recyclerViewPlants.adapter = null
                binding.statusMessage.text = "Aucune plantule trouvée."
                binding.buttonSupprimerBottom.isEnabled = false
            }
        }

        // Handle supprimer click
        binding.buttonSupprimerBottom.setOnClickListener {
            val plantule = lastFoundPlant
            if (plantule != null) {
                val db = DataBaseHelper(this)
                val successArchive = db.addPlantuleToArchive(plantule)
                val successDelete = db.deletePlantuleById(plantule.idPlante)

                if (successArchive && successDelete) {
                    Toast.makeText(this, "Plantule archivée avec succès.", Toast.LENGTH_SHORT).show()
                    binding.recyclerViewPlants.adapter = null
                    binding.buttonSupprimerBottom.isEnabled = false
                    binding.statusMessage.text = "La plantule a été archivée et supprimée."
                } else {
                    Toast.makeText(this, "Erreur lors de l'archivage ou la suppression.", Toast.LENGTH_SHORT).show()
                }
            }
        }
    }

    private fun retourMenuPrincipal() {
        val intent = Intent(this, Menuprincipal::class.java)
        startActivity(intent)
        finish()
    }
}
