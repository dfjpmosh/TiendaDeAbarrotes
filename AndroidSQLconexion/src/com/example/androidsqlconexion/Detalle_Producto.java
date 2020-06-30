package com.example.androidsqlconexion;

import java.util.ArrayList;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

public class Detalle_Producto extends Activity{
	
	private Button btnAgregar;
	private String nom;
	private String id;
	private float precio;
	private int cant;
	private float subt;
	variables_publicas variables = new variables_publicas();
	private Button irCarrito;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_detalle_producto);
		
		Bundle bundle = getIntent().getExtras();
		variables.usuario = bundle.getString("usuario");
		
		id = bundle.getString("id");
		precio = bundle.getFloat("precio");
		nom = bundle.getString("nombre");
		
		TextView nombre = (TextView)findViewById(R.id.nomproducto);
		nombre.setText(bundle.getString("nombre"));
		
		TextView Sprecio = (TextView)findViewById(R.id.preproducto);
		Sprecio.setText("Precio Unitario: " + bundle.getString("Sprecio"));
		
		//Toast.makeText(getBaseContext(),"Categoria: " + cat,Toast.LENGTH_LONG).show();
		
		btnAgregar = (Button)findViewById(R.id.btnAgregarCarrito);
		
		btnAgregar.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				EditText cantidad = (EditText)findViewById(R.id.canproducto);
				String sCant = cantidad.getText().toString();
				cant = Integer.parseInt(sCant);
				subt = cant * precio;
				
				if(cant > 0)
				{
					final String NAMESPACE = "http://suarpe.com/";
					final String URL=variables.direccionIp;// + "/ServicioClientes.asmx"; 
					final String METHOD_NAME = "insPedidoAndroid";
					final String SOAP_ACTION = "http://suarpe.com/insPedidoAndroid";								
	
					SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME);
					
					request.addProperty("idCliente", variables.usuario);
					request.addProperty("nomProducto", nom);
					request.addProperty("idProducto", id);
					request.addProperty("cantidad", cant);
					
					SoapSerializationEnvelope envelope = 
							new SoapSerializationEnvelope(SoapEnvelope.VER11);
					
					envelope.dotNet = true; 
	
					envelope.setOutputSoapObject(request);
	
					HttpTransportSE transporte = new HttpTransportSE(URL);
	
					try 
					{
						transporte.call(SOAP_ACTION, envelope);
	
						/*SoapObject resultado_xml =(SoapObject)envelope.getResponse();
						//SoapObject soapresult = (SoapObject)resultado_xml.getProperty(0);
						
						for(int i=0; i< resultado_xml.getPropertyCount(); i++)
						{
							SoapObject soapresult = (SoapObject)resultado_xml.getProperty(i);
							//lP.add(soapresult.getProperty(1).toString()); 
							lP.add(new Productos(soapresult.getProperty(0).toString(), soapresult.getProperty(1).toString(), Float.parseFloat(soapresult.getProperty(4).toString())));
						}*/
						Toast.makeText(getBaseContext(),"Producto Agregado",Toast.LENGTH_LONG).show();
						finish();
					} 
					catch (Exception e) 
					{
						Toast.makeText(getBaseContext(),e.toString(),Toast.LENGTH_LONG).show();
					}
				}
				else
					Toast.makeText(getBaseContext(),"Ingrese una cantidad Valida",Toast.LENGTH_LONG).show();
			}			
		});
		
		irCarrito = (Button)findViewById(R.id.btnDPIrCarrito);
		
		irCarrito.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				lanzarCarrito();
			}
			
		});		
	}
	
	public void lanzarCarrito() {
  		//Toast.makeText(getBaseContext(),"Categoria: " + cat,Toast.LENGTH_LONG).show();
  		Intent i = new Intent(this, Lista_Carrito.class);
  		i.putExtra("usuario", variables.usuario);
        startActivity(i);        
        //finish();
  	}
}
