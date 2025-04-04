package com.example.toto

import android.app.DatePickerDialog
import android.content.Intent
import android.os.Bundle
import android.widget.EditText
import androidx.appcompat.app.AppCompatActivity
import com.example.toto.databinding.AjouterUnePlantuleBinding
import java.util.Calendar

class AjouterUnePlantule : AppCompatActivity() {

    private lateinit var binding: AjouterUnePlantuleBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = AjouterUnePlantuleBinding.inflate(layoutInflater)
        setContentView(binding.root)

        binding.btnAjouterPlant.setOnClickListener {
            if (validateInput()) {
                ajouterPlante()
            } else {
                // Afficher un message d'erreur à l'utilisateur
                println("Veuillez remplir tous les champs.")
            }
        }

        binding.btnAnnulerPlant.setOnClickListener {
            retourMenuPrincipal()
        }

        // Ajoutez les click listeners pour les EditTexts des dates
        binding.tbDateAjout.setOnClickListener {
            showDatePickerDialog(binding.tbDateAjout)
        }

        binding.tbDateRetrait.setOnClickListener {
            showDatePickerDialog(binding.tbDateRetrait)
        }
    }

    private fun showDatePickerDialog(editText: EditText) {
        val calendar = Calendar.getInstance()
        val year = calendar.get(Calendar.YEAR)
        val month = calendar.get(Calendar.MONTH)
        val day = calendar.get(Calendar.DAY_OF_MONTH)

        val datePickerDialog = DatePickerDialog(
            this,
            { _, selectedYear, selectedMonth, selectedDay ->
                val selectedDate = "$selectedDay/${selectedMonth + 1}/$selectedYear"
                editText.setText(selectedDate)
            },
            year, month, day
        )
        datePickerDialog.show()
    }

    private fun validateInput(): Boolean {
        // Implémenter la validation des champs selon les besoins
        return true
    }

    private fun ajouterPlante() {
        // Récupérer les valeurs des champs
        val identifiant = binding.tbIdentifiant.text.toString()
        val dateAjout = binding.tbDateAjout.text.toString()
        val dateRetrait = binding.tbDateRetrait.text.toString()
        val etatSante = binding.cbEtatDeSante.selectedItem.toString()
        val provenance = binding.tbProvenance.text.toString()
        val description = binding.tbDescription.text.toString()
        val stade = binding.cbStade.selectedItem.toString()
        val entreposage = binding.cbEntreposage.selectedItem.toString()
        val responsableDecontamination = binding.cbResponsableDecontamination.selectedItem.toString()

        // Ajouter les données à un DataGrid ou à une base de données (à implémenter selon les besoins)

        // Exemple : Envoi des données à une autre activité (Menu principal dans cet exemple)
        val intent = Intent(this@AjouterUnePlantule, Menuprincipal::class.java)
        startActivity(intent)
        finish() // Termine l'activité en cours (AjouterUnePlantule)
    }

    private fun retourMenuPrincipal() {
        // Retourner à l'activité MenuPrincipalActivity sans rien faire
        val intent = Intent(this@AjouterUnePlantule, Menuprincipal::class.java)
        startActivity(intent)
        finish() // Termine l'activité en cours (AjouterUnePlantule)
    }
}
