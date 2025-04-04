package com.example.toto

import android.content.Intent
import android.os.Bundle
import androidx.appcompat.app.AppCompatActivity
import com.example.toto.databinding.ArchivesBinding // Assurez-vous d'importer correctement votre binding

class Archives : AppCompatActivity() {

    private lateinit var binding:ArchivesBinding // Utilisez le bon nom pour votre binding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ArchivesBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Configurer les vues et le comportement ici si nécessaire
    }

    private fun retourMenuPrincipal() {
        // Retourner à l'activité MenuPrincipalActivity
        val intent = Intent(this, Menuprincipal::class.java)
        startActivity(intent)
        finish() // Termine l'activité en cours (ArchivesActivity)
    }
}
