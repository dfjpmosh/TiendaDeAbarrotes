package com.example.androidsqlconexion;

import java.util.ArrayList;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import com.example.androidsqlconexion.Menu_productos.AdapterProductos;

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
import android.widget.EditText;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;
import android.widget.AdapterView.OnItemClickListener;

public class Lista_Carrito extends Activity {
	
	variables_publicas variables = new variables_publicas();
	private ListView milista;
	private TextView prod;
	private TextView idProd;
	private TextView nomProd;
	private TextView total;
	private float tot=0;
	private ArrayList<DetallePedido> lP;
	private Button genPedido;

	@Override
	protected void onCreate(Bundle savedInstanceState) {
		// TODO Auto-generated method stub
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_carrito);
		
		Bundle bundle = getIntent().getExtras();
		variables.usuario = bundle.getString("usuario");
		
		milista = (ListView) findViewById(R.id.listaCarrito);
		prod = (TextView) findViewById(R.id.prodCarrito);
		total = (TextView) findViewById(R.id.total);
		
		//lP = new ArrayList<String>();
		lP = new ArrayList<DetallePedido>();
		
		final String NAMESPACE = "http://suarpe.com/";
		final String URL=variables.direccionIp;// + "/ServicioClientes.asmx"; 
		final String METHOD_NAME = "LeerPedidosAndroid";
		final String SOAP_ACTION = "http://suarpe.com/LeerPedidosAndroid";								

		SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME);
		
		request.addProperty("idCliente", bundle.getString("usuario"));

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
				lP.add(new DetallePedido(soapresult.getProperty(1).toString(),soapresult.getProperty(2).toString(), Integer.parseInt(soapresult.getProperty(3).toString()) , Float.parseFloat(soapresult.getProperty(4).toString()), Float.parseFloat(soapresult.getProperty(5).toString())));
				tot += lP.get(i).subtotal;
			}
			String t = String.format("$%.2f", tot);
			total.setText("Total: "+t);
		} 
		catch (Exception e) 
		{
			Toast.makeText(getBaseContext(),e.toString(),Toast.LENGTH_LONG).show();
		} 
		 
	
		
		//ArrayAdapter<String> adaptador = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, lP);
		AdapterProductos adaptador = new AdapterProductos(this);
		
		milista.setAdapter(adaptador);
		
		/*milista.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
				//prod.setText("Producto: ");
				//Toast.makeText(getBaseContext(),"Producto: " + milista.getItemAtPosition(position),Toast.LENGTH_LONG).show();
				//lanzar(lP.get(position).getId(),lP.get(position).getNombre(),lP.get(position).getSprecio(),lP.get(position).getPrecio());
			}
		});*/
		
		genPedido = (Button)findViewById(R.id.btnGenPedido);
		
		genPedido.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				
				final String NAMESPACE = "http://suarpe.com/";
				final String URL=variables.direccionIp;// + "/ServicioClientes.asmx"; 
				final String METHOD_NAME = "insPedido";
				final String SOAP_ACTION = "http://suarpe.com/insPedido";								

				SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME);
				
				request.addProperty("idCliente", variables.usuario);
				
				SoapSerializationEnvelope envelope = 
						new SoapSerializationEnvelope(SoapEnvelope.VER11);
				
				envelope.dotNet = true; 

				envelope.setOutputSoapObject(request);

				HttpTransportSE transporte = new HttpTransportSE(URL);

				try //
				{
					transporte.call(SOAP_ACTION, envelope);

					Toast.makeText(getBaseContext(),"Pedido Realizado",Toast.LENGTH_LONG).show();
					finish();
				} 
				catch (Exception e) 
				{
					Toast.makeText(getBaseContext(),e.toString(),Toast.LENGTH_LONG).show();
				}
				
				
			}
			
		});
	}
	
	class AdapterProductos extends ArrayAdapter<DetallePedido>{
		Activity context;
		
		public AdapterProductos(Activity context){
			super(context, R.layout.activity_carro, lP);
			this.context = context;
		}
		
		public View getView(int posicion, View view, ViewGroup parent){
			LayoutInflater inflater = context.getLayoutInflater();
			View item = inflater.inflate(R.layout.activity_carro, null);
			
			TextView nombre = (TextView)item.findViewById(R.id.nProd);
			nombre.setText(lP.get(posicion).getNomProducto());
			
			TextView cantidad = (TextView)item.findViewById(R.id.cProd);
			cantidad.setText(lP.get(posicion).getsCantidad());
			
			TextView subtotal = (TextView)item.findViewById(R.id.sProd);
			subtotal.setText(lP.get(posicion).getsSubtotal());
			
			return item;
		}
	}
	
	
}
