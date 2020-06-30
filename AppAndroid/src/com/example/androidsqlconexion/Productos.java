package com.example.androidsqlconexion;

public class Productos {
	
	private String id;
	private String nombre;
	private float precio;
	private String Sprecio;
	
	public Productos(String id, String nombre, float precio) {
		this.id = id;
		this.nombre = nombre;
		this.precio = precio;
		this.Sprecio = String.format("$%.2f", precio);
	}

	public String getId() {
		return id;
	}

	public void setId(String id) {
		this.id = id;
	}

	public String getNombre() {
		return nombre;
	}

	public void setNombre(String nombre) {
		this.nombre = nombre;
	}

	public float getPrecio() {
		return precio;
	}

	public void setPrecio(float precio) {
		this.precio = precio;
	}
	
	public String getSprecio() {
		return Sprecio;
	}

	public void setSprecio(String Sprecio) {
		this.id = Sprecio;
	}

}
