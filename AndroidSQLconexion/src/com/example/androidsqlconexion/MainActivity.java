package com.example.androidsqlconexion;


import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapPrimitive;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;



import android.os.Bundle;
import android.app.Activity;
import android.content.Intent;
import android.view.Menu;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Toast;

public class MainActivity extends Activity {

	private Button btnIngresar;
	private EditText txtUsuario;
	private EditText txtPassword;
	
	public final int dialogo_alert=0;
	public String msje="";
	
	// referencia a la clase
	variables_publicas variables=new variables_publicas();

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_main);
		
		txtUsuario = (EditText)findViewById(R.id.txtUsuario);
		txtPassword = (EditText)findViewById(R.id.txtPassword);
		btnIngresar = (Button)findViewById(R.id.btnIngresar);
		
		btnIngresar.setOnClickListener(new OnClickListener() {
		   
			@Override
			public void onClick(View v) {
							
				final String NAMESPACE = "http://suarpe.com/";
				final String URL=variables.direccionIp;//"/ServicioClientes.asmx"; 
				final String METHOD_NAME = "LoginUsuario";
				final String SOAP_ACTION = "http://suarpe.com/LoginUsuario";								

				SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME);

				request.addProperty("user", txtUsuario.getText().toString()); 
				request.addProperty("password", txtPassword.getText().toString()); 

				SoapSerializationEnvelope envelope = 
						new SoapSerializationEnvelope(SoapEnvelope.VER11);
				
				envelope.dotNet = true; 

				envelope.setOutputSoapObject(request);

				HttpTransportSE transporte = new HttpTransportSE(URL);

				try 
				{
					transporte.call(SOAP_ACTION, envelope);
	
					SoapPrimitive resultado_xml =(SoapPrimitive)envelope.getResponse();
					String res = resultado_xml.toString();
					msje=res;
					// mostramos la respuesta en un toast
					Toast.makeText(getBaseContext(), res+"",Toast.LENGTH_SHORT).show();					
			 										
					if(res.equals("Gracias por Iniciar Sesion")){
						variables.usuario=txtUsuario.getText().toString();
						txtUsuario.setText("");
						txtPassword.setText("");
							//envia al otro activity
						Intent intent=new Intent("android.intent.action.Barra_cargado");
						intent.putExtra("usuario", variables.usuario);
						startActivity(intent);
						finish();
					}
					
				} 
				catch (Exception e) 
				{
					Toast.makeText(getBaseContext(),"Fallo Consulta: " + e.toString(),Toast.LENGTH_LONG).show();
				} 
				 
			}
		});
		
		
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
		getMenuInflater().inflate(R.menu.activity_main, menu);
		return true;
	}

}
