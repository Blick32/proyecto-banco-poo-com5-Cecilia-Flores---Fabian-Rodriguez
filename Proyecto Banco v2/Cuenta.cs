using System;
using System.Collections.Generic;

namespace tp_integrador_poo
{
	/// <summary>
	/// Description of Cuenta.
	/// </summary>
	public class Cuenta
	{
		//-------------------ATRIBUTOS---------------------
	
		private int nroCuenta;
		private string dniTitular;
		private double saldo;
		private string apellido;
		
		 //-----------PROPIEDADES---------------------------------------------
	
		public int NroCuenta { get { return nroCuenta; } set { nroCuenta = value; } }
		public string DniTitular { get { return dniTitular; } set { dniTitular = value; } }
		public double Saldo {
			get { return saldo; } 
			set {
				//definir el dedondeo a 2 dígitos
				double valor = Math.Round(value,2);
				saldo = valor; // <--- CORREGIDO (antes decía saldo = value)
			}
		}
		public string Apellido { get { return apellido; } set { apellido = value; } }
		
		
		//----------------CONSTRUCTOR------------------------------------------------------
	
		public Cuenta(int nroCuenta, string dniTitular, string apellido, double saldoInicial)
		{
			//se modifican los datos desde las propiedades
			NroCuenta = nroCuenta;
			DniTitular = dniTitular;
			Apellido = apellido;
			Saldo = saldoInicial;
		}

	
		//------------------------METODOS--------------------------------------------
		
		public void Depositar(double monto)
		{	
			if(double.IsNaN(monto)){
				throw new FormatException("Agregar un valor numérico.");
			}else if(monto <= 0){
				throw new ArgumentOutOfRangeException("Agregar un monto mayor a cero.");
			}else{
				//se modifica desde la propiedad Saldo
				double valor = Math.Round(monto,2);//redondear a 2 dígitos
				Saldo += valor;
			}
		}
	
		public void Extraer(double monto)
		{
			if(double.IsNaN(monto)){
				throw new FormatException("Agregar un valor numérico.");
			}else if(monto <= 0){
				throw new ArgumentOutOfRangeException("Agregar un monto mayor a cero.");
			}else{
				//se modifica desde la propiedad Saldo
				double valor = Math.Round(monto,2);//redondear a 2 dígitos
				Saldo -= valor;
			}
		}
		
		//validar valores ingresados
		

	}
}
