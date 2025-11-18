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
	
	    //retorna un cliente teniendo el indice
	    public Cliente verCliente(int i)
	    {
	    	return clientes[i];
	    }
	    
	    public bool existeCliente(Cliente c)
	    {
	    	return clientes.Contains(c);
	    }
	
	    public List<Cliente> ListarClientes()
	    {
	        return clientes;
	    }
	
	    public int CantidadClientes()
	    {
	    	return clientes.Count;
	    }
	
	    //este no va
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
	
	    public List<Cuenta> ListarCuentas()
	    {
	        return cuentas;
	    }
	
	    public int CantidadCuentas()
	    {
	    	return cuentas.Count;
	    }
	    
	    //este no va
	    public void LimpiarCuentas()
	    {
	        cuentas.Clear();
	    }
	    
	    //agregar recuperar elemento en una posicion //correcion ok
	    public Cuenta verCuenta(int i)
	    {
	    	return cuentas[i];
	    }
	    //existe elemento //correcion ok
	    public bool existeCuenta(Cuenta c)
	    {
	    	return cuentas.Contains(c);
	    }

	}
}
