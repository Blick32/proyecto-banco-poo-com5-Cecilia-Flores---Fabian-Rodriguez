using System;
using System.Collections.Generic;

namespace tp_integrador_poo
{
	/// <summary>
	/// Description of Banco.
	/// </summary>
	public class Banco
	{
		//---------PROPIEDADES------------------
	
		private string nombre;
		
		public string Nombre
		{
			get { return nombre; }
			
			//agregar una exception
			set { 
				nombre = value;
			}
		}
 
		//-----------LISTAS-----------------------------
		
		private List<Cliente> clientes;
		private List<Cuenta> cuentas;
 
		//----------------CONSTRUCTORES-------------------------
		
		public Banco(string nombre)
		{
			//se accede a la propiedad para entrar a la validacion del tipo de dato
			Nombre = nombre;
			
			//se crean las listas en el constructor para que no de null
			clientes = new List<Cliente>();
			cuentas = new List<Cuenta>();
		}
		
		
		 //-----------------MÉTODOS CLIENTES---------------------------------------
	
		public void AgregarCliente(Cliente c)
		{
			clientes.Add(c);
		}
	
		public void EliminarCliente(Cliente c)
		{
			clientes.Remove(c);
		}
	
		public Cliente BuscarCliente(string dni)
		{
			StringNoVacio(dni);
			if(!(dni.Length >= 7 && dni.Length <= 8)){
				throw new ArgumentOutOfRangeException("Ingrese entre 7 y 8 dígitos para el DNI. Por favor, ingrese un valor válido.");
			}
			foreach(Cliente c in clientes){
				if(c.Dni == dni){
					//si el dni del objeto cliente es igual al dni ingresado
					return c;
				}else{
					// <--- CORREGIDO: Se quitaron los Console.WriteLine de aquí
				}
			}
			
			return null;
			//return BuscarClienteRecursivo(dni,0);
		}
		
		//metodo auxiliar
		/*
		private Cliente BuscarClienteRecursivo(string dni, int i){
			if (i >= clientes.Count)
				return null;
			if (clientes[i].Dni == dni)
				return clientes[i];
			return BuscarClienteRecursivo(dni, i + 1);
		}
		*/
	
		public List<Cliente> ListarClientes()
		{
			//aca hay que recorrer la lista y mostrar la informacion impresa en consola
			return clientes;
		}
	
		public int CantidadClientes()
		{
			return clientes.Count;
		}
	
		public void LimpiarClientes()
		{
			clientes.Clear();
		}
		
		 //-------------------METODOS CUENTAS--------------------------
	
		public void AgregarCuenta(Cuenta c)
		{
			cuentas.Add(c);
		}
	
		public void EliminarCuenta(Cuenta c)
		{
			//buscar cuentas del cliente
			cuentas.Remove(c);
		}
	
		public Cuenta BuscarCuenta(int nroCuenta)
		{
			//recorrer con un foreach y buscar con un if
			//return cuentas.Find(x => x.NroCuenta == nroCuenta);
			
			//aca hay que recorrer la lista con un foreach
			foreach(Cuenta c in cuentas){
				if(c.NroCuenta == nroCuenta){
					return c;
				}
			}
			return null;
		}
	
		public List<Cuenta> ListarCuentas()
		{
			return cuentas;
		}
	
		public int CantidadCuentas()
		{
			return cuentas.Count;
		}
	
		public void LimpiarCuentas()
		{
			cuentas.Clear();
		}

	
		
		//metodo para evitar string vacio
		private void StringNoVacio(string valor){
			//validar vacio o nulo
			if(valor == null || valor.Trim() == "" ){
				throw new FormatException("Por favor ingrese un valor.");
			}
		}
		
		
		//-------------------------------- MÉTODOS RECURSIVOS ----------------------------------

		public Cliente BuscarClienteRecursivo(string dni)
	{
		if (string.IsNullOrWhiteSpace(dni))
		return null;

		return BuscarClienteRec(dni, 0);
	}
	

		private Cliente BuscarClienteRec(string dni, int indice)
	{
		if (indice >= clientes.Count)
		return null;

		if (clientes[indice].Dni == dni)
		return clientes[indice];

		return BuscarClienteRec(dni, indice + 1);
}

	}
}
