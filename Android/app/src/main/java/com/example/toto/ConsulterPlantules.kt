package com.example.toto

import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.toto.databinding.ConsulterPlantesBinding

class ConsulterPlantules : AppCompatActivity() {

    private lateinit var binding: ConsulterPlantesBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ConsulterPlantesBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.recyclerViewPlants.layoutManager = LinearLayoutManager(this)

        binding.buttonSearch.setOnClickListener {
            val id = binding.searchId.text.toString().trim()
            if (id.isEmpty()) {
                Toast.makeText(this, "Veuillez entrer un ID", Toast.LENGTH_SHORT).show()
            } else {
                displayPlantuleById(id)
            }
        }
    }

    private fun displayPlantuleById(id: String) {
        val db = DataBaseHelper(this)
        val plant = db.getPlantuleById(id)

        if (plant != null) {
            val adapter = PlantuleAdapter(listOf(plant))
            binding.recyclerViewPlants.adapter = adapter
            binding.statusMessage.text = "Plante trouvée."
        } else {
            binding.statusMessage.text = "Aucune plante trouvée avec l'ID \"$id\"."
            binding.recyclerViewPlants.adapter = null
        }
    }
}
