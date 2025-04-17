    package com.example.toto;

    import android.content.ContentValues;
    import android.content.Context;
    import android.database.Cursor;
    import android.database.sqlite.SQLiteDatabase;
    import android.database.sqlite.SQLiteOpenHelper;

    import androidx.annotation.Nullable;

    import java.util.ArrayList;
    import java.util.List;

    public class DataBaseHelper extends SQLiteOpenHelper {

        public static final String PLANTE = "Plante";

        public static final String ID_PLANTE = "idPlante";
        public static final String ETAT_SANTE = "etatSante";
        public static final String DATE_AJOUT = "dateAjout";
        public static final String PROVENANCE = "provenance";
        public static final String DESCRIPTION = "description";
        public static final String STADE = "stade";
        public static final String ENTREPOSAGE = "entreposage";
        public static final String ACTIVE_INACTIVE = "active_Inactive";
        public static final String ITEM_RETIRE_INVENTAIRE = "itemRetireInventaire";
        public static final String NOTE = "note";
        public static final String RESPONSABLE = "Responsable";

        public DataBaseHelper(@Nullable Context context) {
            super(context, "Canabis.db", null, 1);
        }

        @Override
        public void onCreate(SQLiteDatabase db) {
            // Entreposage
            db.execSQL("CREATE TABLE Entreposage (" +
                    "idEntreposage VARCHAR(255) PRIMARY KEY," +
                    "nom VARCHAR(255)" +
                    ");");

            // CompteUtilisateur
            db.execSQL("CREATE TABLE CompteUtilisateur (" +
                    "idUtilisateur VARCHAR(255) PRIMARY KEY," +
                    "prenom VARCHAR(255)," +
                    "nom VARCHAR(255)," +
                    "Email VARCHAR(255)," +
                    "motdepass VARCHAR(255)" +
                    ");");

            // Responsable
            db.execSQL("CREATE TABLE Responsable (" +
                    "nom VARCHAR(255) PRIMARY KEY" +
                    ");");

            // Plante
            db.execSQL("CREATE TABLE Plante (" +
                    "idPlante VARCHAR(255) PRIMARY KEY," +
                    "etatSante VARCHAR(255)," +
                    "dateAjout DATE," +
                    "provenance VARCHAR(255)," +
                    "description TEXT," +
                    "stade VARCHAR(255)," +
                    "entreposage VARCHAR(255)," +
                    "active_Inactive INT," +
                    "itemRetireInventaire VARCHAR(255)," +
                    "note TEXT," +
                    "Responsable VARCHAR(255)," +
                    "FOREIGN KEY (entreposage) REFERENCES Entreposage(idEntreposage)" +
                    ");");

            // PlanteArchive
            db.execSQL("CREATE TABLE PlanteArchive (" +
                    "idPlante VARCHAR(255) PRIMARY KEY," +
                    "etatSante VARCHAR(255)," +
                    "dateAjout DATE," +
                    "provenance VARCHAR(255)," +
                    "description TEXT," +
                    "stade VARCHAR(255)," +
                    "entreposage VARCHAR(255)," +
                    "active_Inactive INT," +
                    "itemRetireInventaire VARCHAR(255)," +
                    "note TEXT," +
                    "Responsable VARCHAR(255)," +
                    "dateRetrait DATE," +
                    "FOREIGN KEY (entreposage) REFERENCES Entreposage(idEntreposage)" +
                    ");");

            // HistoriquePlante with AUTOINCREMENT
            db.execSQL("CREATE TABLE HistoriquePlante (" +
                    "id INTEGER PRIMARY KEY AUTOINCREMENT," +
                    "idPlante VARCHAR(255)," +
                    "action VARCHAR(255)," +
                    "Date DATE," +
                    "champ VARCHAR(255)," +
                    "ancienneValeur VARCHAR(255)," +
                    "nouvelleValeur VARCHAR(255)" +
                    ");");

            // PlanteCounter
            db.execSQL("CREATE TABLE PlanteCounter (" +
                    "nombreDePlante INT NOT NULL PRIMARY KEY" +
                    ");");
        }

        @Override
        public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {
            db.execSQL("DROP TABLE IF EXISTS Plante");
            db.execSQL("DROP TABLE IF EXISTS PlanteArchive");
            db.execSQL("DROP TABLE IF EXISTS Entreposage");
            db.execSQL("DROP TABLE IF EXISTS CompteUtilisateur");
            db.execSQL("DROP TABLE IF EXISTS Responsable");
            db.execSQL("DROP TABLE IF EXISTS HistoriquePlante");
            db.execSQL("DROP TABLE IF EXISTS PlanteCounter");
            onCreate(db);
        }

        // Add new plant
        public boolean addPlantule(PlantuleModel plantuleModel) {
            SQLiteDatabase db = this.getWritableDatabase();
            ContentValues cv = new ContentValues();

            cv.put(ID_PLANTE, plantuleModel.getIdPlante());
            cv.put(ETAT_SANTE, plantuleModel.getEtatSante());
            cv.put(DATE_AJOUT, plantuleModel.getDateAjout());
            cv.put(PROVENANCE, plantuleModel.getProvenance());
            cv.put(DESCRIPTION, plantuleModel.getDescription());
            cv.put(STADE, plantuleModel.getStade());
            cv.put(ENTREPOSAGE, plantuleModel.getEntreposage());
            cv.put(ACTIVE_INACTIVE, plantuleModel.getActive_Inactive());
            cv.put(ITEM_RETIRE_INVENTAIRE, plantuleModel.getItemRetireInventaire());
            cv.put(NOTE, plantuleModel.getNote());
            cv.put(RESPONSABLE, plantuleModel.getResponsable());

            long insert = db.insert(PLANTE, null, cv);
            db.close();
            return insert != -1;
        }

        // Update plant
        public boolean updatePlantule(PlantuleModel plantuleModel) {
            SQLiteDatabase db = this.getWritableDatabase();
            ContentValues cv = new ContentValues();

            cv.put(ETAT_SANTE, plantuleModel.getEtatSante());
            cv.put(DATE_AJOUT, plantuleModel.getDateAjout());
            cv.put(PROVENANCE, plantuleModel.getProvenance());
            cv.put(DESCRIPTION, plantuleModel.getDescription());
            cv.put(STADE, plantuleModel.getStade());
            cv.put(ENTREPOSAGE, plantuleModel.getEntreposage());
            cv.put(ACTIVE_INACTIVE, plantuleModel.getActive_Inactive());
            cv.put(ITEM_RETIRE_INVENTAIRE, plantuleModel.getItemRetireInventaire());
            cv.put(NOTE, plantuleModel.getNote());
            cv.put(RESPONSABLE, plantuleModel.getResponsable());

            int result = db.update(PLANTE, cv, ID_PLANTE + "=?", new String[]{plantuleModel.getIdPlante()});
            db.close();
            return result > 0;
        }

        // Get plant by ID
        public PlantuleModel getPlantuleById(String idPlante) {
            SQLiteDatabase db = this.getReadableDatabase();
            PlantuleModel plantule = null;

            String query = "SELECT * FROM " + PLANTE + " WHERE " + ID_PLANTE + " = ?";
            Cursor cursor = db.rawQuery(query, new String[]{idPlante});

            if (cursor != null && cursor.moveToFirst()) {
                String etatSante = cursor.getString(cursor.getColumnIndexOrThrow(ETAT_SANTE));
                String dateAjout = cursor.getString(cursor.getColumnIndexOrThrow(DATE_AJOUT));
                String provenance = cursor.getString(cursor.getColumnIndexOrThrow(PROVENANCE));
                String description = cursor.getString(cursor.getColumnIndexOrThrow(DESCRIPTION));
                String stade = cursor.getString(cursor.getColumnIndexOrThrow(STADE));
                String entreposage = cursor.getString(cursor.getColumnIndexOrThrow(ENTREPOSAGE));
                int active_Inactive = cursor.getInt(cursor.getColumnIndexOrThrow(ACTIVE_INACTIVE));
                String itemRetireInventaire = cursor.getString(cursor.getColumnIndexOrThrow(ITEM_RETIRE_INVENTAIRE));
                String note = cursor.getString(cursor.getColumnIndexOrThrow(NOTE));
                String responsable = cursor.getString(cursor.getColumnIndexOrThrow(RESPONSABLE));

                plantule = new PlantuleModel(
                        idPlante,
                        responsable,
                        note,
                        itemRetireInventaire,
                        active_Inactive,
                        stade,
                        description,
                        provenance,
                        dateAjout,
                        etatSante,
                        entreposage
                );
            }

            if (cursor != null) {
                cursor.close();
            }
            db.close();
            return plantule;
        }

        // Count total plants
        public int getNombreTotalDePlantes() {
            SQLiteDatabase db = this.getReadableDatabase();
            String query = "SELECT COUNT(*) FROM " + PLANTE;
            Cursor cursor = db.rawQuery(query, null);
            int total = 0;

            if (cursor.moveToFirst()) {
                total = cursor.getInt(0);
            }

            cursor.close();
            db.close();
            return total;
        }

        // Add entry to HistoriquePlante
        public boolean enregistrerHistoriqueAjout(String idPlante) {
            SQLiteDatabase db = this.getWritableDatabase();
            ContentValues cv = new ContentValues();

            cv.put("idPlante", idPlante.toUpperCase());
            cv.put("action", "ajout");

            // Compatible with all Android versions
            String today = new java.text.SimpleDateFormat("yyyy-MM-dd", java.util.Locale.getDefault()).format(new java.util.Date());
            cv.put("Date", today);

            cv.put("champ", "--");
            cv.put("ancienneValeur", "__");
            cv.put("nouvelleValeur", "__");

            long insert = db.insert("HistoriquePlante", null, cv);
            db.close();
            return insert != -1;
        }

        public List<HistoriqueModel> getHistoriqueByPlantId(String idPlante) {
            List<HistoriqueModel> list = new ArrayList<>();
            SQLiteDatabase db = this.getReadableDatabase();

            Cursor cursor = db.rawQuery("SELECT * FROM HistoriquePlante WHERE idPlante = ?", new String[]{idPlante});

            if (cursor.moveToFirst()) {
                do {
                    int id = cursor.getInt(cursor.getColumnIndexOrThrow("id"));
                    String action = cursor.getString(cursor.getColumnIndexOrThrow("action"));
                    String date = cursor.getString(cursor.getColumnIndexOrThrow("Date"));
                    String champ = cursor.getString(cursor.getColumnIndexOrThrow("champ"));
                    String ancienne = cursor.getString(cursor.getColumnIndexOrThrow("ancienneValeur"));
                    String nouvelle = cursor.getString(cursor.getColumnIndexOrThrow("nouvelleValeur"));

                    list.add(new HistoriqueModel(id, idPlante, action, date, champ, ancienne, nouvelle));
                } while (cursor.moveToNext());
            }

            cursor.close();
            db.close();
            return list;
        }

        public boolean addPlantuleToArchive(PlantuleModel plant) {
            SQLiteDatabase db = this.getWritableDatabase();
            ContentValues cv = new ContentValues();

            cv.put("idPlante", plant.getIdPlante());
            cv.put("etatSante", plant.getEtatSante());
            cv.put("dateAjout", plant.getDateAjout());
            cv.put("provenance", plant.getProvenance());
            cv.put("description", plant.getDescription());
            cv.put("stade", plant.getStade());
            cv.put("entreposage", plant.getEntreposage());
            cv.put("active_Inactive", plant.getActive_Inactive());
            cv.put("itemRetireInventaire", plant.getItemRetireInventaire());
            cv.put("note", plant.getNote());
            cv.put("Responsable", plant.getResponsable());

            // dateRetrait is optional — you can set it to today if you want:
            String today = new java.text.SimpleDateFormat("yyyy-MM-dd", java.util.Locale.getDefault()).format(new java.util.Date());
            cv.put("dateRetrait", today);

            long result = db.insert("PlanteArchive", null, cv);
            db.close();
            return result != -1;
        }

        public boolean deletePlantuleById(String idPlante) {
            SQLiteDatabase db = this.getWritableDatabase();
            int deleted = db.delete("Plante", "idPlante = ?", new String[]{idPlante});
            db.close();
            return deleted > 0;
        }

        public List<PlantuleModel> getAllPlantules() {
            List<PlantuleModel> plantules = new ArrayList<>();
            SQLiteDatabase db = this.getReadableDatabase();
            Cursor cursor = db.rawQuery("SELECT * FROM " + PLANTE, null);

            if (cursor.moveToFirst()) {
                do {
                    PlantuleModel plantule = new PlantuleModel(
                            cursor.getString(cursor.getColumnIndexOrThrow(ID_PLANTE)),
                            cursor.getString(cursor.getColumnIndexOrThrow(RESPONSABLE)),
                            cursor.getString(cursor.getColumnIndexOrThrow(NOTE)),
                            cursor.getString(cursor.getColumnIndexOrThrow(ITEM_RETIRE_INVENTAIRE)),
                            cursor.getInt(cursor.getColumnIndexOrThrow(ACTIVE_INACTIVE)),
                            cursor.getString(cursor.getColumnIndexOrThrow(STADE)),
                            cursor.getString(cursor.getColumnIndexOrThrow(DESCRIPTION)),
                            cursor.getString(cursor.getColumnIndexOrThrow(PROVENANCE)),
                            cursor.getString(cursor.getColumnIndexOrThrow(DATE_AJOUT)),
                            cursor.getString(cursor.getColumnIndexOrThrow(ETAT_SANTE)),
                            cursor.getString(cursor.getColumnIndexOrThrow(ENTREPOSAGE))
                    );
                    plantules.add(plantule);
                } while (cursor.moveToNext());
            }

            cursor.close();
            db.close();
            return plantules;
        }

        public List<PlantuleModel> getAllArchivedPlantules() {
            List<PlantuleModel> list = new ArrayList<>();
            SQLiteDatabase db = this.getReadableDatabase();
            Cursor cursor = db.rawQuery("SELECT * FROM PlanteArchive", null);

            if (cursor.moveToFirst()) {
                do {
                    list.add(new PlantuleModel(
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
                    ));
                } while (cursor.moveToNext());
            }

            cursor.close();
            db.close();
            return list;
        }

        public PlantuleModel getArchivedPlantuleById(String idPlante) {
            SQLiteDatabase db = this.getReadableDatabase();
            PlantuleModel plantule = null;
            Cursor cursor = db.rawQuery("SELECT * FROM PlanteArchive WHERE idPlante = ?", new String[]{idPlante});
            if (cursor != null && cursor.moveToFirst()) {
                plantule = new PlantuleModel(
                        idPlante,
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
                );
                cursor.close();
            }
            db.close();
            return plantule;
        }

        public boolean enregistrerHistorique(String idPlante, String action, String champ, String ancienne, String nouvelle, String date) {
            SQLiteDatabase db = this.getWritableDatabase();
            ContentValues cv = new ContentValues();
            cv.put("idPlante", idPlante);
            cv.put("action", action);
            cv.put("champ", champ);
            cv.put("ancienneValeur", ancienne);
            cv.put("nouvelleValeur", nouvelle);
            cv.put("Date", date);
            long result = db.insert("HistoriquePlante", null, cv);
            db.close();
            return result != -1;
        }

    }
