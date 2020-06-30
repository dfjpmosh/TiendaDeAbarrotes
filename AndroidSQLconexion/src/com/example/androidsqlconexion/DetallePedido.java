package com.example.androidsqlconexion;

public class DetallePedido{
	public String nomProducto;
	public String idProducto;
    public int cantidad;
    public float precio;
    public float subtotal;
    public String sSubtotal;
    public String sCantidad;
    
	public DetallePedido(String nomProducto, String idProducto, int cantidad, float precio, float subtotal) {
		this.nomProducto = nomProducto;
		this.idProducto = idProducto;
		this.cantidad = cantidad;
		this.precio = precio;
		this.subtotal = subtotal;
		this.sCantidad = String.valueOf(cantidad);
		this.sSubtotal = String.format("$%.2f", subtotal);
	}
	
	public String getNomProducto() {
		return nomProducto;
	}

	public void setNomProducto(String idProducto) {
		this.nomProducto = nomProducto;
	}

	public String getIdProducto() {
		return idProducto;
	}

	public void setIdProducto(String idProducto) {
		this.idProducto = idProducto;
	}

	public int getCantidad() {
		return cantidad;
	}

	public void setCantidad(int cantidad) {
		this.cantidad = cantidad;
	}

	public float getPrecio() {
		return precio;
	}

	public void setPrecio(float precio) {
		this.precio = precio;
	}

	public float getSubtotal() {
		return subtotal;
	}

	public void setSubtotal(float subtotal) {
		this.subtotal = subtotal;
	}

	public String getsSubtotal() {
		return sSubtotal;
	}

	public void setsSubtotal(String sSubtotal) {
		this.sSubtotal = sSubtotal;
	}

	public String getsCantidad() {
		return sCantidad;
	}

	public void setsCantidad(String sCantidad) {
		this.sCantidad = sCantidad;
	}
	
    
    
}
