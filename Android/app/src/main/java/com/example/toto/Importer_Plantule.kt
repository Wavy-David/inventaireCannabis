package com.example.toto

import android.app.Activity
import android.content.Intent
import android.net.Uri
import android.os.Bundle
import android.widget.Toast
import androidx.appcompat.app.AppCompatActivity
import androidx.recyclerview.widget.LinearLayoutManager
import com.example.toto.databinding.ImporterPlantuleBinding
import org.apache.poi.ss.usermodel.*
import org.apache.poi.ss.usermodel.CellType.*
import java.io.InputStream

class Importer_Plantule : AppCompatActivity() {

    private lateinit var binding: ImporterPlantuleBinding
    private lateinit var dbHelper: DataBaseHelper
    private val REQUEST_CODE = 1001

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        binding = ImporterPlantuleBinding.inflate(layoutInflater)
        setContentView(binding.root)

        dbHelper = DataBaseHelper(this)
        binding.recyclerView.layoutManager = LinearLayoutManager(this)

        binding.buttonBrowse.setOnClickListener {
            val intent = Intent(Intent.ACTION_GET_CONTENT)
            intent.type = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
            startActivityForResult(intent, REQUEST_CODE)
        }
    }

    override fun onActivityResult(requestCode: Int, resultCode: Int, data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)

        if (requestCode == REQUEST_CODE && resultCode == Activity.RESULT_OK && data != null) {
            val uri: Uri = data.data!!
            val inputStream: InputStream? = contentResolver.openInputStream(uri)

            try {
                if (inputStream != null) {
                    val workbook = WorkbookFactory.create(inputStream)
                    val sheet = workbook.getSheetAt(0)
                    var successCount = 0

                    for (i in 1..sheet.lastRowNum) {
                        val row = sheet.getRow(i)
                        if (row != null) {
                            val etatSante = getCellString(row.getCell(0))
                            val dateAjout = getCellString(row.getCell(1))
                            val idPlante = getCellString(row.getCell(2))
                            val provenance = getCellString(row.getCell(3))
                            val description = getCellString(row.getCell(4))
                            val stade = getCellString(row.getCell(5))
                            val entreposage = getCellString(row.getCell(6))
                            val actifInactif = getCellInt(row.getCell(7))
                            val motifRetrait = getCellString(row.getCell(8))
                            val note = getCellString(row.getCell(9))
                            val responsable = getCellString(row.getCell(10))

                            val plantule = PlantuleModel(
                                idPlante,
                                responsable,
                                note,
                                motifRetrait,
                                actifInactif,
                                stade,
                                description,
                                provenance,
                                dateAjout,
                                etatSante,
                                entreposage
                            )

                            if (dbHelper.addPlantule(plantule)) {
                                successCount++
                            }
                        }
                    }

                    inputStream.close()

                    if (successCount > 0) {
                        val allPlants = getAllPlantsFromDB()
                        binding.recyclerView.adapter = PlantuleAdapter(allPlants)
                        Toast.makeText(this, "$successCount plantules ajoutées.", Toast.LENGTH_SHORT).show()
                    } else {
                        Toast.makeText(this, "Aucune donnée ajoutée.", Toast.LENGTH_SHORT).show()
                    }
                }
            } catch (e: Exception) {
                Toast.makeText(this, "Erreur: ${e.message}", Toast.LENGTH_LONG).show()
            }
        }
    }

    private fun getAllPlantsFromDB(): List<PlantuleModel> {
        val list = mutableListOf<PlantuleModel>()
        val db = dbHelper.readableDatabase
        val cursor = db.rawQuery("SELECT * FROM Plante", null)

        if (cursor.moveToFirst()) {
            do {
                list.add(
                    PlantuleModel(
                        cursor.getString(cursor.getColumnIndexOrThrow("idPlante")),
                        cursor.getString(cursor.getColumnIndexOrThrow("Responsable")),
                        cursor.getString(cursor.getColumnIndexOrThrow("note")),
                        cursor.getString(cursor.getColumnIndexOrThrow("itemRetireInventaire")),
                        cursor.getInt(cursor.getColumnIndexOrThrow("active_Inactive")),
                        cursor.getString(cursor.getColumnIndexOrThrow("stade")),
                        cursor.getString(cursor.getColumnIndexOrThrow("description")),
                        cursor.getString(cursor.getColumnIndexOrThrow("provenance")),
                        cursor.getString(cursor.getColumnIndexOrThrow("dateAjout")),
                        cursor.getString(cursor.getColumnIndexOrThrow("etatSante")),
                        cursor.getString(cursor.getColumnIndexOrThrow("entreposage"))
                    )
                )
            } while (cursor.moveToNext())
        }

        cursor.close()
        db.close()
        return list
    }

    private fun getCellString(cell: Cell?): String {
        if (cell == null) return ""
        return when (cell.cellType) {
            STRING -> cell.stringCellValue
            NUMERIC -> cell.numericCellValue.toString()
            BOOLEAN -> cell.booleanCellValue.toString()
            else -> ""
        }
    }

    private fun getCellInt(cell: Cell?): Int {
        if (cell == null) return 0
        return when (cell.cellType) {
            NUMERIC -> cell.numericCellValue.toInt()
            STRING -> cell.stringCellValue.toIntOrNull() ?: 0
            BOOLEAN -> if (cell.booleanCellValue) 1 else 0
            else -> 0
        }
    }
}
