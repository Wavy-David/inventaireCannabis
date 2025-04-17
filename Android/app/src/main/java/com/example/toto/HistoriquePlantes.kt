package com.example.toto

import android.os.Bundle
import android.widget.Button
import android.widget.EditText
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import androidx.recyclerview.widget.RecyclerView

class HistoriquePlantes : AppCompatActivity() {

    private lateinit var searchBox: EditText
    private lateinit var searchButton: Button
    private lateinit var recyclerView: RecyclerView
    private lateinit var statusMessage: TextView
    private lateinit var dbHelper: DataBaseHelper

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.historique_plantes)

        dbHelper = DataBaseHelper(this)

        searchBox = findViewById(R.id.search_id)
        searchButton = findViewById(R.id.button_search)
        recyclerView = findViewById(R.id.recycler_view_plants)
        statusMessage = findViewById(R.id.status_message)

        recyclerView.layoutManager = LinearLayoutManager(this)

        searchButton.setOnClickListener {
            val id = searchBox.text.toString().trim()
            if (id.isNotEmpty()) {
                val historiqueList = dbHelper.getHistoriqueByPlantId(id.uppercase())

                if (historiqueList.isNotEmpty()) {
                    val adapter = HistoriqueAdapter(historiqueList)
                    recyclerView.adapter = adapter
                    adapter.notifyDataSetChanged()
                    statusMessage.text = "${historiqueList.size} historique(s) trouvé(s) pour $id"
                } else {
                    recyclerView.adapter = null
                    statusMessage.text = "Aucun historique trouvé pour ID: $id"
                }
            } else {
                recyclerView.adapter = null
                statusMessage.text = "Veuillez entrer un ID valide"
            }
        }
    }
}
