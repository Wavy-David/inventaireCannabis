package com.example.toto

import android.content.Intent
import android.os.Bundle
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.toto.databinding.ModifierPlantuleBinding

class ModifierPlante : AppCompatActivity() {

    private lateinit var binding: ModifierPlantuleBinding

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ModifierPlantuleBinding.inflate(layoutInflater)
        setContentView(binding.root)

        // Spinner setup (needed to use setSelection properly)
        setupSpinners()

        // Search button click
        binding.btnSearch.setOnClickListener {
            val id = binding.searchId.text.toString().trim()
            if (id.isNotEmpty()) {
                loadPlantuleData(id)
            } else {
                Toast.makeText(this, "Veuillez entrer un identifiant", Toast.LENGTH_SHORT).show()
            }
        }

        //button sauvegarder
        binding.btnSave.setOnClickListener {
            updatePlantule()
        }

        // Cancel button
        binding.btnCancel.setOnClickListener {
            retourMenuPrincipal()
        }
    }

    private fun setupSpinners() {
        // These should match the arrays in res/values/strings.xml
        val etatAdapter = ArrayAdapter.createFromResource(this, R.array.etat_sante, android.R.layout.simple_spinner_item)
        etatAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        binding.cbEtatDeSante.adapter = etatAdapter

        val stadeAdapter = ArrayAdapter.createFromResource(this, R.array.motifs_retrait, android.R.layout.simple_spinner_item)
        stadeAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        binding.cbStade.adapter = stadeAdapter

        val entreposageAdapter = ArrayAdapter.createFromResource(this, R.array.entreposage, android.R.layout.simple_spinner_item)
        entreposageAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        binding.cbEntreposage.adapter = entreposageAdapter

        val responsableAdapter = ArrayAdapter.createFromResource(this, R.array.responsables_decontamination, android.R.layout.simple_spinner_item)
        responsableAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        binding.cbResponsableDecontamination.adapter = responsableAdapter

        val retraitAdapter = ArrayAdapter.createFromResource(this, R.array.raison_retrait, android.R.layout.simple_spinner_item)
        retraitAdapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item)
        binding.cbRaisonRetrait.adapter = retraitAdapter
    }

    private fun loadPlantuleData(id: String) {
        val db = DataBaseHelper(this)
        val plantule = db.getPlantuleById(id.uppercase())

        if (plantule == null) {
            Toast.makeText(this, "Plante introuvable", Toast.LENGTH_LONG).show()
            return
        }

        binding.tbIdentifiant.setText(plantule.idPlante)
        binding.tbDateAjout.setText(plantule.dateAjout)
        binding.tbProvenance.setText(plantule.provenance)
        binding.tbDescription.setText(plantule.description)
        binding.tbNotes.setText(plantule.note)

        // Spinners: select item by value
        setSpinnerSelection(binding.cbEtatDeSante, plantule.etatSante)
        setSpinnerSelection(binding.cbStade, plantule.stade)
        setSpinnerSelection(binding.cbEntreposage, plantule.entreposage)
        setSpinnerSelection(binding.cbResponsableDecontamination, plantule.responsable)
        setSpinnerSelection(binding.cbRaisonRetrait, plantule.itemRetireInventaire)

        // Radio buttons
        if (plantule.active_Inactive == 1) {
            binding.radioActif.isChecked = true
        } else {
            binding.radioInactif.isChecked = true
        }

        Toast.makeText(this, "Plante chargée", Toast.LENGTH_SHORT).show()
    }

    private fun setSpinnerSelection(spinner: android.widget.Spinner, value: String) {
        val adapter = spinner.adapter
        for (i in 0 until adapter.count) {
            if (adapter.getItem(i).toString().equals(value, ignoreCase = true)) {
                spinner.setSelection(i)
                break
            }
        }
    }

    private fun updatePlantule() {
        val id = binding.tbIdentifiant.text.toString().trim()
        if (id.isEmpty()) {
            Toast.makeText(this, "Aucun identifiant à modifier.", Toast.LENGTH_SHORT).show()
            return
        }

        val db = DataBaseHelper(this)

        val etatSante = binding.cbEtatDeSante.selectedItem.toString()
        val dateAjout = binding.tbDateAjout.text.toString()
        val provenance = binding.tbProvenance.text.toString()
        val description = binding.tbDescription.text.toString()
        val stade = binding.cbStade.selectedItem.toString()
        val entreposage = binding.cbEntreposage.selectedItem.toString()
        val responsable = binding.cbResponsableDecontamination.selectedItem.toString()
        val note = binding.tbNotes.text.toString()
        val raisonRetrait = binding.cbRaisonRetrait.selectedItem.toString()

        val isActive = if (binding.radioActif.isChecked) 1 else 0

        val updatedPlantule = PlantuleModel(
            id,
            responsable,
            note,
            raisonRetrait,
            isActive,
            stade,
            description,
            provenance,
            dateAjout,
            etatSante,
            entreposage
        )

        val success = db.updatePlantule(updatedPlantule)
        if (success) {
            Toast.makeText(this, "Plante mise à jour avec succès!", Toast.LENGTH_LONG).show()
        } else {
            Toast.makeText(this, "Échec de la mise à jour.", Toast.LENGTH_LONG).show()
        }
    }

    private fun retourMenuPrincipal() {
        val intent = Intent(this, Menuprincipal::class.java)
        startActivity(intent)
        finish()
    }
}
