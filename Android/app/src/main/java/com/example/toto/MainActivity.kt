package com.example.toto

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Button
import android.widget.EditText

class MainActivity : AppCompatActivity() {

    lateinit var username_input: EditText
    lateinit var password_input: EditText
    lateinit var login_button: Button


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        username_input=findViewById(R.id.username_input)
        password_input=findViewById(R.id.password_input)
        login_button=findViewById(R.id.login_button)

        login_button.setOnClickListener {
            val username = username_input.text.toString()
            val password = password_input.text.toString()
            Log.i("Test Credentials", "Username: $username and Password: $password")

            // Intent to start Menuprincipal
            val intent = Intent(this@MainActivity, Menuprincipal::class.java)
            startActivity(intent)
        }
    }
}