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
    var isActive: Int = 0
    var idPlant: String = ""

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = AjouterUnePlantuleBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Ajouter une plante
        binding.btnAjouterPlant.setOnClickListener {
            if (validateInput()) {
                ajouterPlante()
            } else {
                println("Veuillez remplir tous les champs.")
            }
        }

        // Annuler
        binding.btnAnnulerPlant.setOnClickListener {
            retourMenuPrincipal()
        }

        // Choix de la date
        binding.tbDateAjout.setOnClickListener {
            showDatePickerDialog(binding.tbDateAjout)
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
        return true // You can improve this later
    }

    private fun ajouterPlante() {
        val db = DataBaseHelper(this)

        val dateAjout = binding.tbDateAjout.text.toString()
        val etatSante = binding.cbEtatDeSante.selectedItem.toString()
        val provenance = binding.tbProvenance.text.toString()
        val description = binding.tbDescription.text.toString()
        val stade = binding.cbStade.selectedItem.toString()
        val entreposage = binding.cbEntreposage.selectedItem.toString()
        val responsable = binding.cbResponsableDecontamination.selectedItem.toString()
        val note = binding.tbNote.text.toString()
        val retrait = binding.cbRaisonRetrait.selectedItem.toString()

        isActive = when {
            binding.radioActif.isChecked -> 1
            binding.radioInactif.isChecked -> 0
            else -> 0
        }

        idPlant = "SLH" + (db.getNombreTotalDePlantes() + 1)

        val plantule = PlantuleModel(
            idPlant,
            responsable,
            note,
            retrait,
            isActive,
            stade,
            description,
            provenance,
            dateAjout,
            etatSante,
            entreposage
        )

        val success = db.addPlantule(plantule)
        println("✅ Plante insert success? --> $success")

        if (success) {
            val historiqueSuccess = db.enregistrerHistoriqueAjout(idPlant)
            println("📚 Historique insert success? --> $historiqueSuccess")
        }

        val intent = Intent(this@AjouterUnePlantule, Menuprincipal::class.java)
        startActivity(intent)
        finish()
    }

    private fun retourMenuPrincipal() {
        val intent = Intent(this, Menuprincipal::class.java)
        startActivity(intent)
        finish()
    }
}
