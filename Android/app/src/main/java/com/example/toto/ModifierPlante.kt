package com.example.toto

import android.content.Intent
import android.os.Bundle
import android.widget.ArrayAdapter
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import com.example.toto.databinding.ModifierPlantuleBinding
import java.text.SimpleDateFormat
import java.util.*

class ModifierPlante : AppCompatActivity() {

    private lateinit var binding: ModifierPlantuleBinding
    private lateinit var originalPlantule: PlantuleModel

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ModifierPlantuleBinding.inflate(layoutInflater)
        setContentView(binding.root)

        setupSpinners()

        binding.btnSearch.setOnClickListener {
            val id = binding.searchId.text.toString().trim()
            if (id.isNotEmpty()) {
                loadPlantuleData(id)
            } else {
                Toast.makeText(this, "Veuillez entrer un identifiant", Toast.LENGTH_SHORT).show()
            }
        }

        binding.btnSave.setOnClickListener {
            updatePlantule()
        }

        binding.btnCancel.setOnClickListener {
            retourMenuPrincipal()
        }
    }

    private fun setupSpinners() {
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

        originalPlantule = plantule

        binding.tbIdentifiant.setText(plantule.idPlante)
        binding.tbDateAjout.setText(plantule.dateAjout)
        binding.tbProvenance.setText(plantule.provenance)
        binding.tbDescription.setText(plantule.description)
        binding.tbNotes.setText(plantule.note)

        setSpinnerSelection(binding.cbEtatDeSante, plantule.etatSante)
        setSpinnerSelection(binding.cbStade, plantule.stade)
        setSpinnerSelection(binding.cbEntreposage, plantule.entreposage)
        setSpinnerSelection(binding.cbResponsableDecontamination, plantule.responsable)
        setSpinnerSelection(binding.cbRaisonRetrait, plantule.itemRetireInventaire)

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

        val newEtatSante = binding.cbEtatDeSante.selectedItem.toString()
        val newDateAjout = binding.tbDateAjout.text.toString()
        val newProvenance = binding.tbProvenance.text.toString()
        val newDescription = binding.tbDescription.text.toString()
        val newStade = binding.cbStade.selectedItem.toString()
        val newEntreposage = binding.cbEntreposage.selectedItem.toString()
        val newResponsable = binding.cbResponsableDecontamination.selectedItem.toString()
        val newNote = binding.tbNotes.text.toString()
        val newRaisonRetrait = binding.cbRaisonRetrait.selectedItem.toString()
        val newIsActive = if (binding.radioActif.isChecked) 1 else 0

        val updatedPlantule = PlantuleModel(
            id,
            newResponsable,
            newNote,
            newRaisonRetrait,
            newIsActive,
            newStade,
            newDescription,
            newProvenance,
            newDateAjout,
            newEtatSante,
            newEntreposage
        )

        val success = db.updatePlantule(updatedPlantule)
        if (success) {
            val changes = mutableListOf<String>()
            val oldValues = mutableListOf<String>()
            val newValues = mutableListOf<String>()

            fun checkChange(field: String, oldVal: String, newVal: String) {
                if (oldVal != newVal) {
                    changes.add(field)
                    oldValues.add(oldVal)
                    newValues.add(newVal)
                }
            }

            checkChange("etatSante", originalPlantule.etatSante, newEtatSante)
            checkChange("dateAjout", originalPlantule.dateAjout, newDateAjout)
            checkChange("provenance", originalPlantule.provenance, newProvenance)
            checkChange("description", originalPlantule.description, newDescription)
            checkChange("stade", originalPlantule.stade, newStade)
            checkChange("entreposage", originalPlantule.entreposage, newEntreposage)
            checkChange("responsable", originalPlantule.responsable, newResponsable)
            checkChange("note", originalPlantule.note, newNote)
            checkChange("itemRetireInventaire", originalPlantule.itemRetireInventaire, newRaisonRetrait)
            checkChange("active_Inactive", originalPlantule.active_Inactive.toString(), newIsActive.toString())

            val champ = changes.joinToString(", ")
            val ancienneValeur = oldValues.joinToString(", ")
            val nouvelleValeur = newValues.joinToString(", ")
            val currentDate = SimpleDateFormat("yyyy-MM-dd", Locale.getDefault()).format(Date())

            db.enregistrerHistorique(
                id,
                "Mis à jour / modifier",
                champ.ifEmpty { "__" },
                ancienneValeur.ifEmpty { "__" },
                nouvelleValeur.ifEmpty { "__" },
                currentDate
            )

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
