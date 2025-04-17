package com.example.toto

import android.view.LayoutInflater
import android.view.View
import android.view.ViewGroup
import android.widget.TextView
import androidx.recyclerview.widget.RecyclerView

class PlantuleAdapter(private val plantList: List<PlantuleModel>) :
    RecyclerView.Adapter<PlantuleAdapter.PlantuleViewHolder>() {

    class PlantuleViewHolder(itemView: View) : RecyclerView.ViewHolder(itemView) {
        val tvId: TextView = itemView.findViewById(R.id.tvId)
        val tvEtatSante: TextView = itemView.findViewById(R.id.tvEtatSante)
        val tvDateAjout: TextView = itemView.findViewById(R.id.tvDateAjout)
        val tvProvenance: TextView = itemView.findViewById(R.id.tvProvenance)
        val tvDescription: TextView = itemView.findViewById(R.id.tvDescription)
        val tvStade: TextView = itemView.findViewById(R.id.tvStade)
        val tvEntreposage: TextView = itemView.findViewById(R.id.tvEntreposage)
        val tvActif: TextView = itemView.findViewById(R.id.tvActif)
        val tvRetrait: TextView = itemView.findViewById(R.id.tvRetrait)
        val tvNote: TextView = itemView.findViewById(R.id.tvNote)
        val tvResponsable: TextView = itemView.findViewById(R.id.tvResponsable)
    }

    override fun onCreateViewHolder(parent: ViewGroup, viewType: Int): PlantuleViewHolder {
        val view = LayoutInflater.from(parent.context).inflate(R.layout.item_plantule, parent, false)
        return PlantuleViewHolder(view)
    }

    override fun onBindViewHolder(holder: PlantuleViewHolder, position: Int) {
        val plant = plantList[position]
        holder.tvId.text = "ID: ${plant.idPlante}"
        holder.tvEtatSante.text = "État de santé: ${plant.etatSante}"
        holder.tvDateAjout.text = "Date d'ajout: ${plant.dateAjout}"
        holder.tvProvenance.text = "Provenance: ${plant.provenance}"
        holder.tvDescription.text = "Description: ${plant.description}"
        holder.tvStade.text = "Stade: ${plant.stade}"
        holder.tvEntreposage.text = "Entreposage: ${plant.entreposage}"
        holder.tvActif.text = "Statut: ${if (plant.active_Inactive == 1) "Actif" else "Inactif"}"
        holder.tvRetrait.text = "Motif retrait: ${plant.itemRetireInventaire}"
        holder.tvNote.text = "Note: ${plant.note}"
        holder.tvResponsable.text = "Responsable: ${plant.responsable}"
    }

    override fun getItemCount(): Int = plantList.size
}
