package com.example.toto

import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.toto.databinding.ConsulterPlantesBinding

class ConsulterPlantules : AppCompatActivity() {
    private lateinit var binding: ConsulterPlantesBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ConsulterPlantesBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Configurer les vues et le comportement ici si nécessaire
    }

    private fun retourMenuPrincipal() {
        // Retourner à l'activité MenuPrincipal
        val intent = Intent(this, Menuprincipal::class.java)
        startActivity(intent)
        finish() // Termine l'activité en cours (ConsulterPlantules)
    }
}
