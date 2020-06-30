package com.example.androidsqlconexion;

import java.util.ArrayList;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

public class Menu_productos extends Activity {
	
	variables_publicas variables = new variables_publicas();
	private ListView milista;
	private TextView prod;
	private TextView idProd;
	private TextView nomProd;
	//private ArrayList<String> lP;
	private ArrayList<Productos> lP;
	private Button irCarrito;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_menu_productos);
		
		Bundle bundle = getIntent().getExtras();
		//variables.usuario = bundle.getString("usuario");
		
		milista = (ListView) findViewById(R.id.listaProd);
		prod = (TextView) findViewById(R.id.producto);
		
		//lP = new ArrayList<String>();
		lP = new ArrayList<Productos>();
		
		final String NAMESPACE = "http://suarpe.com/";
		final String URL=variables.direccionIp;// + "/ServicioClientes.asmx"; 
		final String METHOD_NAME = "LeerProductos";
		final String SOAP_ACTION = "http://suarpe.com/LeerProductos";								

		SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME);
		
		request.addProperty("Cat", bundle.getString("categoria"));

		SoapSerializationEnvelope envelope = 
				new SoapSerializationEnvelope(SoapEnvelope.VER11);
		
		envelope.dotNet = true; 

		envelope.setOutputSoapObject(request);

		HttpTransportSE transporte = new HttpTransportSE(URL);

		try 
		{
			transporte.call(SOAP_ACTION, envelope);

			SoapObject resultado_xml =(SoapObject)envelope.getResponse();
			//SoapObject soapresult = (SoapObject)resultado_xml.getProperty(0);
			
			for(int i=0; i< resultado_xml.getPropertyCount(); i++)
			{
				SoapObject soapresult = (SoapObject)resultado_xml.getProperty(i);
				//lP.add(soapresult.getProperty(1).toString()); 
				lP.add(new Productos(soapresult.getProperty(0).toString(), soapresult.getProperty(1).toString(), Float.parseFloat(soapresult.getProperty(4).toString())));
			}
			
		} 
		catch (Exception e) 
		{
			Toast.makeText(getBaseContext(),e.toString(),Toast.LENGTH_LONG).show();
		} 
		 
	
		
		//ArrayAdapter<String> adaptador = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, lP);
		AdapterProductos adaptador = new AdapterProductos(this);
		
		milista.setAdapter(adaptador);
		
		milista.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
				//prod.setText("Producto: ");
				//Toast.makeText(getBaseContext(),"Producto: " + milista.getItemAtPosition(position),Toast.LENGTH_LONG).show();
				lanzar(lP.get(position).getId(),lP.get(position).getNombre(),lP.get(position).getSprecio(),lP.get(position).getPrecio());
			}
		});
		
		irCarrito = (Button)findViewById(R.id.btnProIrCarrito);
		
		irCarrito.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				lanzarCarrito();
			}
			
		});
	}
	
	class AdapterProductos extends ArrayAdapter<Productos>{
		Activity context;
		
		public AdapterProductos(Activity context){
			super(context, R.layout.activity_producto, lP);
			this.context = context;
		}
		
		public View getView(int posicion, View view, ViewGroup parent){
			LayoutInflater inflater = context.getLayoutInflater();
			View item = inflater.inflate(R.layout.activity_producto, null);
			
			TextView nombre = (TextView)item.findViewById(R.id.nombre);
			nombre.setText(lP.get(posicion).getNombre());
			
			TextView precio = (TextView)item.findViewById(R.id.precio);
			precio.setText(lP.get(posicion).getSprecio());
			
			return item;
		}
	}
	
	public void lanzar(String id, String nom, String Spre, float precio) {
  		//Toast.makeText(getBaseContext(),"Categoria: " + cat,Toast.LENGTH_LONG).show();
  		Intent i = new Intent(this, Detalle_Producto.class);
  		i.putExtra("id", id);
  		i.putExtra("nombre", nom);
  		i.putExtra("Sprecio", Spre);
  		i.putExtra("precio", precio);
  		i.putExtra("usuario", variables.usuario);
  		startActivity(i);        
        //finish();
  	}
	
	public void lanzarCarrito() {
  		//Toast.makeText(getBaseContext(),"Categoria: " + cat,Toast.LENGTH_LONG).show();
  		Intent i = new Intent(this, Lista_Carrito.class);
  		i.putExtra("usuario", variables.usuario);
        startActivity(i);        
        //finish();
  	}
}
