package com.example.androidsqlconexion;


import java.util.ArrayList;

import org.ksoap2.SoapEnvelope;
import org.ksoap2.serialization.SoapObject;
import org.ksoap2.serialization.SoapPrimitive;
import org.ksoap2.serialization.SoapSerializationEnvelope;
import org.ksoap2.transport.HttpTransportSE;

import android.R.string;
import android.os.Bundle;
import android.app.Activity;
import android.app.AlertDialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.util.Log;
import android.view.KeyEvent;
import android.view.Menu;
import android.view.MenuItem;
import android.view.SubMenu;
import android.view.View;
import android.view.WindowManager;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.ListView;
import android.widget.TextView;
import android.widget.Toast;

public class Menu_principal extends Activity {
	
	variables_publicas variables=new variables_publicas();
	private ListView milista;
	private TextView cat;
	private ArrayList<String> lCat;
	private Button irCarrito;
	
	@Override
	protected void onCreate(Bundle savedInstanceState) {
		super.onCreate(savedInstanceState);
		setContentView(R.layout.activity_menu_principal);
		
		Bundle bundle = getIntent().getExtras();
		variables.usuario = bundle.getString("usuario");
		
		milista = (ListView) findViewById(R.id.lista);
		cat = (TextView) findViewById(R.id.categoria);
		
		lCat = new ArrayList<String>();
		
		/*for(int i=0; i<30; i++){
			Coleccion.add(Integer.toString(i));
		}*/
		
		final String NAMESPACE = "http://suarpe.com/";
		final String URL=variables.direccionIp;// + "/ServicioClientes.asmx"; 
		final String METHOD_NAME = "LeerCategorias";
		final String SOAP_ACTION = "http://suarpe.com/LeerCategorias";								

		SoapObject request = new SoapObject(NAMESPACE, METHOD_NAME);

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
				lCat.add(soapresult.getProperty(1).toString()); 
			}
			
		} 
		catch (Exception e) 
		{
			Toast.makeText(getBaseContext(),e.toString(),Toast.LENGTH_LONG).show();
		} 
		 
	
		
		ArrayAdapter<String> adaptador = new ArrayAdapter<String>(this, android.R.layout.simple_list_item_1, lCat);
		
		milista.setAdapter(adaptador);
		
		milista.setOnItemClickListener(new OnItemClickListener() {
			@Override
			public void onItemClick(AdapterView<?> parent, View view, int position, long id) {
				//cat.setText("Categoria: ");
				//Toast.makeText(getBaseContext(),"Categoria: " + milista.getItemAtPosition(position),Toast.LENGTH_LONG).show();
				lanzar(milista.getItemAtPosition(position).toString());
			}
		});
		
		irCarrito = (Button)findViewById(R.id.btnPriIrCarrito);
		
		irCarrito.setOnClickListener(new OnClickListener(){

			@Override
			public void onClick(View v) {
				// TODO Auto-generated method stub
				lanzarCarrito();
			}
			
		});
		
	}

	@Override
	public boolean onCreateOptionsMenu(Menu menu) {
		// Inflate the menu; this adds items to the action bar if it is present.
				//getMenuInflater().inflate(R.menu.activity_menu_principal, menu);
				//return true;
				
				//MEN� 1 Y SUBMEN� 1
				 
			       //SubMenu sub1 = menu.addSubMenu(id del grupo, id del item, orden, t�tulo del menu)
			       SubMenu sub1 = menu.addSubMenu(1,1,1,"Salir");
			 
			       //Icono del men� 1
			       sub1.setIcon(R.drawable.salir);
			       //Icono de las opciones del submen� del men� 1
			       sub1.setHeaderIcon(R.drawable.salir);
			 
			       //sub1.add(id del grupo, id del item, orden, t�tulo de la opci�n)
			       //sub1.add(1, 10, 1, "Menu salir opci�n 0");
			       //sub1.add(1, 11, 1, "Menu salir opci�n 1");
			 
			       //MEN� 2 Y SUBMEN� 2 PARECIDO AL ANTERIOR
			 
			       SubMenu sub2 = menu.addSubMenu(2,2,2,"Informaci�n");
			       sub2.setHeaderIcon(R.drawable.info);
			       sub2.setIcon(R.drawable.info);
			 
			       //sub2.add(1, 20, 0, "Men� informaci�n opci�n 0");
			       //sub2.add(1, 21, 1, "Men� informaci�n opci�n 1");
			       /*Como pod�is comprobar en las opciones del men� 1 de id del item le pongo 
			        * a la 1� opci�n 10 y a la 2� 11 debido a que pertenece al men� 1 la opci�n 0 y la opci�n 1 
			        * lo mismo hago con las id de las opciones del 2� men� 20 a la primera y 21 a la segunda.
			        * Esto cada persona lo puede poner como quiera, pero hay que tener cuidado, ya que
			        * no se puede repetir el id de ningun item (opci�n)
			        */
			 
			       return super.onCreateOptionsMenu(menu);
	}
	
	//Gestionar los elementos del men�
    @Override
    public boolean onOptionsItemSelected(MenuItem item){
        /*El switch se encargar� de gestionar cada elemento del men� dependiendo de su id,
        por eso dijimos antes que ning�n id de los elementos del men� podia ser igual.
        */
 
        switch(item.getItemId()){
        case 1: //Id del men�, para combrobar que se ha pulsado       	
        	AlertDialog.Builder builder = new AlertDialog.Builder(this);
        	builder.setMessage("�Desea Salir?")
        	        .setTitle("Advertencia")
        	        .setCancelable(false)
        	        .setPositiveButton("Si",
        	                new DialogInterface.OnClickListener() {
        	                    public void onClick(DialogInterface dialog, int id) {
        	                        // metodo que se debe implementar
        	                    	//envia al otro activity login
        	                    	Intent intent = new Intent(Menu_principal.this, MainActivity.class);
        	                        startActivity(intent);
        	                        finish();
        	                    }
        	                })
        	 .setNegativeButton("No",
 	                new DialogInterface.OnClickListener() {
 	                    public void onClick(DialogInterface dialog, int id) {
 	                        dialog.cancel();
 	                    }
 	                });
        	AlertDialog alert = builder.create();
        	alert.show(); 
            break;
        case 2:
            Toast.makeText(this,"Has pulsado la Opci�n Informacion",Toast.LENGTH_SHORT).show();
            break;     
                
        }
        return true;//Consumimos el item, no se propaga
    }
    
    

    
  //Definimos que para cuando se presione la tecla BACK no volvamos para atras  	 
  	 @Override
  	 public boolean onKeyDown(int keyCode, KeyEvent event)  {
  	     if (keyCode == KeyEvent.KEYCODE_BACK && event.getRepeatCount() == 0) {
  	         // no hacemos nada.
  	         return true;
  	     }

  	   if(keyCode == KeyEvent.KEYCODE_HOME) {
	        Log.i("Home Button","Clicked");
	    }
  	     
  	     return super.onKeyDown(keyCode, event);
  	 }
  	 
  	@Override
  	public void onAttachedToWindow() {
  	    this.getWindow().setType(WindowManager.LayoutParams.TYPE_KEYGUARD);
  	    super.onAttachedToWindow();
  	}
  	
  	public void lanzar(String cat ) {
  		//Toast.makeText(getBaseContext(),"Categoria: " + cat,Toast.LENGTH_LONG).show();
  		Intent i = new Intent(this, Menu_productos.class);
  		i.putExtra("categoria", cat);
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
