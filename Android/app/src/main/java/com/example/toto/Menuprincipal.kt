package com.example.toto

import android.content.Intent
import android.os.Bundle
import android.widget.Button
import android.widget.ImageView
import androidx.appcompat.app.AppCompatActivity

class Menuprincipal : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.menu_principal)

        // Initialisation du bouton de déconnexion
        val logoutButton: Button = findViewById(R.id.logout_button)
        logoutButton.setOnClickListener {
            // Code pour la déconnexion de l'utilisateur
            val intent = Intent(this@Menuprincipal, MainActivity::class.java)
            startActivity(intent)
            finish() // Termine l'activité en cours (Menu principal)
        }

        // Initialisation du bouton Ajouter une plante
        val addButton: ImageView = findViewById(R.id.add_button)
        addButton.setOnClickListener {
            // Lancer l'activité AjouterUnePlantule
            val intent = Intent(this@Menuprincipal, AjouterUnePlantule::class.java)
            startActivity(intent)
        }

        // Initialisation du bouton Importer depuis Excel
        val importerButton: ImageView = findViewById(R.id.importer_button)
        importerButton.setOnClickListener {
            // Lancer l'activité ImporterExcel
            val intent = Intent(this@Menuprincipal,Importer_Plantule::class.java)
            startActivity(intent)
        }

        // Initialisation du bouton Modifier une plante
        val updateButton: ImageView = findViewById(R.id.update_button)
        updateButton.setOnClickListener {
            // Lancer l'activité ModifierPlante
            val intent = Intent(this@Menuprincipal, ModifierPlante ::class.java)
            startActivity(intent)
        }

        // Initialisation du bouton Archives
        val archiveButton: ImageView = findViewById(R.id.archive_button)
        archiveButton.setOnClickListener {
            // Lancer l'activité Archives
            val intent = Intent(this@Menuprincipal,Archives::class.java)
            startActivity(intent)
        }

        // Initialisation du bouton Consulter les plantes
        val consulterButton: ImageView = findViewById(R.id.consulter_button)
        consulterButton.setOnClickListener {
            // Lancer l'activité ConsulterPlantes
            val intent = Intent(this@Menuprincipal, ConsulterPlantules::class.java)
            startActivity(intent)
        }

        // Initialisation du bouton Historique des plantes
        val historiqueButton: ImageView = findViewById(R.id.historique_button)
        historiqueButton.setOnClickListener {
            // Lancer l'activité HistoriquePlantes
            val intent = Intent(this@Menuprincipal,HistoriquePlantules::class.java)
            startActivity(intent)
        }
    }
}
