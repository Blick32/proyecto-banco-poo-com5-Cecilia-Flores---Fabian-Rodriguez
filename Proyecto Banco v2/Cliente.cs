using System;
using System.Collections.Generic;

namespace tp_integrador_poo
{
	/// <summary>
	/// Description of Cliente.
	/// </summary>
	public class Cliente
	{
		//--------------ATRIBUTOS--------------------
	
		private string nombre;
		private string apellido;
		private string dni;
		private string direccion;
		private string telefono;
		private string mail;
		
		//--------------PROPIEDADES--------------------------------------------------
	
		public string Nombre {
			get { return nombre; }
			set {
				//paso el valor por el metodo privado para validar que no este vacio y que no pase numeros
				StringNoVacio(value);
				ValidaTexto(value);
				nombre = value;
			}
		}
		public string Apellido {
			get { return apellido; }
			set {
				StringNoVacio(value);
				ValidaTexto(value);
				apellido = value;
			}
		}
		
		public string Dni {
			get { return dni; }
			set {
				StringNoVacio(value);
				if(!(value.Length >= 7 && value.Length <= 8)){
					throw new IndexOutOfRangeException("Ingrese entre 7 y 8 dígitos para el DNI. Por favor, ingrese un valor válido.");
				}
				dni = value;
			}
		}
		public string Direccion {
			get { return direccion; }
			set {
				StringNoVacio(value);
				direccion = value;
			}
		}
		public string Telefono {
			get { return telefono; }
			set {
				StringNoVacio(value);
				ValidaNumeros(value);
				telefono = value;
			}
		}
		public string Mail { get { return mail; } set { mail = value; } }
		
		
		//------------------CONSTRUCTOR--------------------------------------------------------------
		
		//se crea el cliente con el contructor por defecto, pero lo dejamos explicito porque se necesita utilizar.
		//crea el objeto vacio, luego usamos los metodos de validacion y una vez validados todos los datos desde Program
		//llamamos al metodo CargarDatos.
		public Cliente(){}
		
		
		public Cliente(string nombre, string apellido, string dni, string direccion, string telefono, string mail)
		{
			Nombre = nombre;
			Apellido = apellido;
			Dni = dni;
			Direccion = direccion;
			Telefono = telefono;
			Mail = mail;
		}
		
		public void CargarDatos(string nombre, string apellido, string dni, string direccion, string telefono, string mail)
		{
			this.Nombre = nombre;
			this.Apellido = apellido;
			this.Dni = dni;
			this.Direccion = direccion;
			this.Telefono = telefono;
			this.Mail = mail;
		}
		
		//metodos para validad datos
		private void ValidaTexto(string valor){
			//se recorre el string y se valida si contiene un numero
			//en la primera aparicion lanza la exception
			foreach(char v in valor){
				if(v >= '0' && v <= '9'){
					throw new FormatException("No se permiten números. Ingrese nuevamente.");
				}
			}
		}
		
		//metodo para evitar string vacio
		private void StringNoVacio(string valor){
			//validar vacio o nulo
			if(valor == null || valor.Trim() == "" ){
				throw new FormatException("Por favor ingrese un valor. Ingrese nuevamente.");
			}
		}
		
		private void ValidaNumeros(string valor){
			foreach(char v in valor){
				if(!(v >= '0' && v <= '9')){
					throw new FormatException("Solo se permiten números. Ingrese nuevamente.");
				}
			}
		}

	}
}
