using System;
using System.Collections.Generic;

namespace tp_integrador_poo
{
	class Program
	{
		
		//crear una cuenta
		static void AgregarCuenta(Banco banco)
		{
			Console.WriteLine("=== AGREGAR CUENTA ===");
			
			string dni = "";
			Cliente cliente = null;
			bool dniValido = false;
			do{
				Console.Write("DNI del cliente: ");
				dni = Console.ReadLine();
				try{
					cliente = banco.BuscarCliente(dni);
					dniValido = true;
				}catch(FormatException ex){
					Console.WriteLine(ex.Message);
				}catch(IndexOutOfRangeException ex){
					Console.WriteLine(ex.Message);
				}
			}while(!dniValido);
			
			//si no existe el cliente, se crea uno nuevo
			if (cliente == null)
			{
				cliente = new Cliente();
				//pido los datos del cliente
				
				cliente.Dni = dni; //agrego el dni agregado al inicio para verificar si existe
				
				string nombre = "";
				bool nombreValido = false;
				do{
					Console.Write("Nombre: ");
					nombre = Console.ReadLine();
					try{
						cliente.Nombre = nombre; //propiedad nombre para que afecte en el set
						nombreValido = true;
					}catch(FormatException ex){
						Console.WriteLine(ex.Message);
					}
				}while(!nombreValido);
				
				string apellido = "";
				bool apellidoValido = false;
				do{
					Console.Write("Apellido: ");
					apellido = Console.ReadLine();
					try{
						cliente.Apellido = apellido;
						apellidoValido = true;
					}catch(FormatException ex){
						Console.WriteLine(ex.Message);
					}
				}while(!apellidoValido);
				
				string direccion = "";
				bool direccionValido = false;
				do{
					Console.Write("Dirección: ");
					direccion = Console.ReadLine();
					try{
						cliente.Direccion = direccion;
						direccionValido = true;
					}catch(FormatException ex){
						Console.WriteLine(ex.Message);
					}
				}while(!direccionValido);
				
				string telefono = "";
				bool telefonoValido = false;
				do{
					Console.Write("Teléfono: ");
					telefono = Console.ReadLine();
					try{
						cliente.Telefono = telefono;
						telefonoValido = true;
					}catch(FormatException ex){
						Console.WriteLine(ex.Message);
					}
				}while(!telefonoValido);
				
				string mail = "";
				bool mailValido = false;
				do{
					Console.Write("Mail: ");
					mail = Console.ReadLine();
					try{
						//solo valido que no este vacio
						cliente.Mail = mail;
						mailValido = true;
					}catch(FormatException ex){
						Console.WriteLine(ex.Message);
					}
				}while(!mailValido);

				//agrego cliente
				banco.AgregarCliente(cliente);
			}
			
			double saldoValor = 0;
			bool saldoValido = false;
			do{
				Console.Write("Saldo inicial: ");
				string saldo = Console.ReadLine();
				try{
					//convertir a double
					if(! double.TryParse(saldo, out saldoValor)){
						throw new FormatException("Ingresar número válido.");
					}
					if(saldoValor < 0){
						throw new ArgumentOutOfRangeException("Ingresar valor mayor a cero.");
					}
					saldoValido = true;
				}catch(FormatException ex){
					Console.WriteLine(ex.Message);
				}catch(ArgumentOutOfRangeException ex){
					Console.WriteLine(ex.Message);
				}catch(Exception ex){
					Console.WriteLine(ex.Message);
				}
			}while(!saldoValido);
			
			int nroCuenta = banco.CantidadCuentas() + 1;
			Cuenta cuenta = new Cuenta(nroCuenta, dni, cliente.Apellido, saldoValor);
			banco.AgregarCuenta(cuenta);

			Console.WriteLine("La cuenta fue creada con éxito.");
		}

		//-----------ELIMINAR CUENTAS--------------------------------
		
		static void EliminarCuenta(Banco banco)
		{
			Console.WriteLine("=== ELIMINAR CUENTA ===");
			
			
			//validar dato ingresado que sea numerico
			bool nroValido = false;
			int nroCuenta = 0;
			//string nro = Console.ReadLine(); // <--- ERROR (Estaba afuera del bucle)
			do{
				Console.Write("Ingrese número de cuenta: ");
				string nro = Console.ReadLine(); // <--- CORREGIDO (Movido aquí adentro)
				try{
					if(nro == null || nro.Trim() == "" ){
						throw new FormatException("Por favor ingrese un valor. Ingrese nuevamente.");
					}
					foreach(char v in nro){
						if(!(v >= '0' && v <= '9')){
							throw new FormatException("Solo se permiten números. Ingrese nuevamente.");
						}
					}
					nroCuenta = int.Parse(nro);
					nroValido = true;	
				}catch(FormatException ex){
					Console.WriteLine(ex.Message);
				}catch(Exception ex){
					Console.WriteLine(ex.Message);
				}
			}while(!nroValido);
			
			Cuenta cuenta = banco.BuscarCuenta(nroCuenta);

			if (cuenta != null)
			{
				//recupero el dni del titular de la cuenta, para buscar mas cuentas del cliente
				string dniTitular = cuenta.DniTitular;

				//elimino la cuenta del listado de cuentas
				banco.EliminarCuenta(cuenta);
				Console.WriteLine("Cuenta eliminada con éxito.");
				
				//buscamos si el cliente tiene mas cuentas con la recursiva
				//si el resultado es 0 se elimina el cliente
				if(ContarCuentasCliente(banco.ListarCuentas(), dniTitular, 0) == 0){
					//buscar cliente
					Cliente cliente = banco.BuscarCliente(dniTitular);
					//eliminar cliente
					banco.EliminarCliente(cliente);
					Console.WriteLine("El Cliente fue eliminado, no tiene mas cuentas en este banco.");
				}
			}
			else
			{
				Console.WriteLine("No se encontró la cuenta.");
			}
		}
		//funcion recursiva para contar cuentas de clientes
		static int ContarCuentasCliente(List<Cuenta> cuentas, string dniCliente, int i){
			if(i < cuentas.Count){
				if(cuentas[i].DniTitular == dniCliente){
					return 1 + ContarCuentasCliente(cuentas,dniCliente, i+1);
				}else{
					return ContarCuentasCliente(cuentas,dniCliente,i+1);
				}
			}else{
				return 0;
			}
		}

	
		//------------------MOSTRAR LISTA DE CLIENTES-------------------------------------

		static void ListarClientesConVariasCuentas(Banco banco)
		{
			Console.WriteLine("=== CLIENTES CON VARIAS CUENTAS ===");
			List<Cliente> clientes = banco.ListarClientes();

			foreach (Cliente c in clientes)
			{
				int cantidad = 0;
				List<Cuenta> cuentas = banco.ListarCuentas();

				foreach (Cuenta cuenta in cuentas)
				{
					if (cuenta.DniTitular == c.Dni)
						cantidad++;
				}

				if (cantidad > 1)
				{
					Console.WriteLine(c.Apellido + ", " + c.Nombre + " - DNI: " + c.Dni);
					Console.WriteLine("Tiene " + cantidad + " cuentas.\n");
				}
			}
		}

	
		//-----------------------REALIZAR EXTRACCIONES-------------------------------------------------
		
		static void RealizarExtraccion(Banco banco)
		{
			Console.WriteLine("=== EXTRACCIÓN ===");
			
			// --- CORREGIDO: Inicio validación de Nro de Cuenta ---
			bool nroValido = false;
			int nroCuenta = 0;
			do{
				Console.Write("Número de cuenta: ");
				string nro = Console.ReadLine();
				try{
					// Reutilizamos tu método de validación
					ValidarNumeroEntero(nro);
					nroCuenta = int.Parse(nro);
					nroValido = true;	
				}catch(FormatException ex){
					Console.WriteLine(ex.Message);
				}catch(Exception ex){
					Console.WriteLine(ex.Message);
				}
			}while(!nroValido);
			// --- Fin validación Nro de Cuenta ---

			Cuenta cuenta = banco.BuscarCuenta(nroCuenta);

			if (cuenta != null)
			{
				// --- CORREGIDO: Inicio validación de Monto (Estilo DepositarDinero) ---
				bool montoValido = false;
				double montoDouble = 0;
				do{
					try{
						Console.Write("Monto a extraer: ");
						string monto = Console.ReadLine();
						if(monto == null || monto.Trim() == "" ){
							throw new FormatException("Por favor ingrese un valor válido.");
						}
						monto = monto.Replace('.',',');
						if(!double.TryParse(monto, out montoDouble)){
							throw new FormatException("Por favor ingresa número con decimales.");
						}
						
						// Agregamos la validación lógica que ya tenías en la clase Cuenta
						if(montoDouble <= 0) {
							throw new ArgumentOutOfRangeException("Agregar un monto mayor a cero.");
						}
						
						montoValido = true;
					}catch(FormatException ex){
						Console.WriteLine(ex.Message);
					}catch(ArgumentOutOfRangeException ex){ // Capturamos la nueva excepción
						Console.WriteLine(ex.Message);
					}catch(Exception ex){
						Console.WriteLine(ex.Message);
					}
				}while(!montoValido);
				// --- Fin validación de Monto ---

				if (montoDouble <= cuenta.Saldo)
				{
					cuenta.Extraer(montoDouble);
					Console.WriteLine("Extracción realizada. Saldo actual: " + cuenta.Saldo);
				}
				else
				{
					Console.WriteLine("Saldo insuficiente.");
				}
			}
			else
			{
				Console.WriteLine("Cuenta no encontrada.");
			}
		}


	
		//--------------------------- DEPOSITAR DINERO------------------------------------

		static void ValidarNumeroEntero(string nro){
			
			if(nro == null || nro.Trim() == "" ){
				throw new FormatException("Por favor ingrese un valor válido.");
			}
			//valor numérico
			foreach(char v in nro){
				if(!(v >= '0' && v <= '9')){
					throw new FormatException("Solo se permiten números. Ingrese nuevamente.");
				}
			}
		}
		
		static void DepositarDinero(Banco banco)
		{
			Console.WriteLine("=== DEPÓSITO ===");
			
			
			//validar dato
			bool cuentaValida = false;
			int nroValido = 0;
			do{
				try{
					Console.Write("Número de cuenta: ");
					string nro = Console.ReadLine();
					//valor vacio
					
					//numero positivo
					
					// CORREGIDO: Reutilizamos tu método de validación
					ValidarNumeroEntero(nro); 
					
					//si llega hasta aca
					nroValido = int.Parse(nro);
					cuentaValida = true;
					
				}catch(FormatException ex){
					Console.WriteLine("Formato invalido");
					Console.WriteLine(ex.Message);
				}catch(Exception ex){
					Console.WriteLine("mensaje por dfecto");
					Console.WriteLine(ex.Message);
				}
				
			}while(!cuentaValida);
			
			Cuenta cuenta = banco.BuscarCuenta(nroValido);

			if (cuenta != null)
			{
				bool montoValido = false;
				double montoDouble = 0;
				do{
					try{
						Console.Write("Monto a depositar: ");
						string monto = Console.ReadLine();
						//verificar que no este vacio
						if(monto == null || monto.Trim() == "" ){
							throw new FormatException("Por favor ingrese un valor válido.");
						}
						//verificar que no
						monto = monto.Replace('.',',');//reemplazo el punto por coma, para que no me de error y haga bien las sumas
						if(!double.TryParse(monto, out montoDouble)){
							throw new FormatException("Por favor ingresa número con decimales.");
						}
						
						montoValido = true;
					}catch(FormatException ex){
						Console.WriteLine(ex.Message);
					}catch(Exception ex){
						Console.WriteLine(ex.Message);
					}
				}while(!montoValido);
				cuenta.Depositar(montoDouble);
				Console.WriteLine("Depósito realizado. Saldo actual: " + cuenta.Saldo);
			}
			else
			{
				Console.WriteLine("Cuenta no encontrada.");
			}
		}

	
		//-------------------------------HACER TRANSFERENCIAS------------------------------------------------------------

		
		static void TransferirDinero(Banco banco)
		{
			Console.WriteLine("=== TRANSFERENCIA ===");
			
			// --- CORREGIDO: Validación Cuenta Origen ---
			bool nroValidoOrigen = false;
			int nroOrigen = 0;
			do{
				Console.Write("Cuenta origen: ");
				string nro = Console.ReadLine();
				try{
					ValidarNumeroEntero(nro); // Reutilizamos tu método
					nroOrigen = int.Parse(nro);
					nroValidoOrigen = true;	
				}catch(FormatException ex){
					Console.WriteLine(ex.Message);
				}catch(Exception ex){
					Console.WriteLine(ex.Message);
				}
			}while(!nroValidoOrigen);
			
			// --- CORREGIDO: Validación Cuenta Destino ---
			bool nroValidoDestino = false;
			int nroDestino = 0;
			do{
				Console.Write("Cuenta destino: ");
				string nro = Console.ReadLine();
				try{
					ValidarNumeroEntero(nro); // Reutilizamos tu método
					nroDestino = int.Parse(nro);
					nroValidoDestino = true;	
				}catch(FormatException ex){
					Console.WriteLine(ex.Message);
				}catch(Exception ex){
					Console.WriteLine(ex.Message);
				}
			}while(!nroValidoDestino);


			Cuenta origen = banco.BuscarCuenta(nroOrigen);
			Cuenta destino = banco.BuscarCuenta(nroDestino);

			if (origen != null && destino != null)
			{
				// --- CORREGIDO: Validación Monto (Estilo DepositarDinero) ---
				bool montoValido = false;
				double montoDouble = 0;
				do{
					try{
						Console.Write("Monto a transferir: ");
						string monto = Console.ReadLine();
						if(monto == null || monto.Trim() == "" ){
							throw new FormatException("Por favor ingrese un valor válido.");
						}
						monto = monto.Replace('.',',');
						if(!double.TryParse(monto, out montoDouble)){
							throw new FormatException("Por favor ingresa número con decimales.");
						}
						
						if(montoDouble <= 0) {
							throw new ArgumentOutOfRangeException("Agregar un monto mayor a cero.");
						}
						
						montoValido = true;
					}catch(FormatException ex){
						Console.WriteLine(ex.Message);
					}catch(ArgumentOutOfRangeException ex){
						Console.WriteLine(ex.Message);
					}catch(Exception ex){
						Console.WriteLine(ex.Message);
					}
				}while(!montoValido);
				// --- Fin Monto ---

				if (montoDouble <= origen.Saldo)
				{
					origen.Extraer(montoDouble);
					destino.Depositar(montoDouble);
					Console.WriteLine("Transferencia realizada correctamente.");
				}
				else
				{
					Console.WriteLine("Saldo insuficiente en cuenta origen.");
				}
			}
			else
			{
				Console.WriteLine("Una o ambas cuentas no existen.");
			}
		}

		//---------------- MOSTRAR LISTA DE CUENTAS------------------------------		

		static void ListarCuentas(Banco banco)
		{
			Console.WriteLine("=== LISTADO DE CUENTAS ===");
			List<Cuenta> cuentas = banco.ListarCuentas();

			foreach (Cuenta c in cuentas)
			{
				Console.WriteLine("Cuenta Nº " + c.NroCuenta + " - Titular DNI: " + c.DniTitular + " - Saldo: " + c.Saldo);
			}
		}
		
		
		
		 //------------------ LISTAR CUENTAS (RECURSIVO) ------------------
		static void ListarCuentasRecursivas(Banco banco)
		{
			Console.WriteLine("=== LISTADO DE CUENTAS (RECURSIVO) ===");
			List<Cuenta> cuentas = banco.ListarCuentas();

			if (cuentas.Count == 0)
			{
				Console.WriteLine("No hay cuentas registradas.");
			}
			else
			{
				MostrarCuentasRecursivas(cuentas, 0);
			}
		}

	
		static void MostrarCuentasRecursivas(List<Cuenta> cuentas, int indice)
		{
			if (indice >= cuentas.Count)
				return; 

			Cuenta c = cuentas[indice];
			Console.WriteLine("Cuenta Nº " + c.NroCuenta +
							 " - Titular DNI: " + c.DniTitular +
							 " - Saldo: " + c.Saldo);

		
			MostrarCuentasRecursivas(cuentas, indice + 1);
		}


	
		//-------------------------MOSTRAR LISTA DE CLIENTES--------------------------------------------

		static void ListarClientes(Banco banco)
		{
			Console.WriteLine("=== LISTADO DE CLIENTES ===");
			List<Cliente> clientes = banco.ListarClientes();

			foreach (Cliente c in clientes)
			{
				Console.WriteLine(c.Apellido + ", " + c.Nombre + " - DNI: " + c.Dni);
			}
		}
		
		
		public static void Main(string[] args)
		{
			
			//inicializar banco y cargarle los datos básico de nombre.
			Banco banco;
			banco = new Banco("Comisión");
			
			//Armado de menu
			
			//variable que ingresa el usuario
			//la inicialice en -1 porque si la dejaba en cero, salia directamente y no volvia al menu.
			int opcion = -1;
			do{
				Console.WriteLine("inicia menu");
				//Console.Clear();
				Console.WriteLine("............. MENU PRINCIPAL .............");
				Console.WriteLine("1 - Agregar cuenta");
				Console.WriteLine("2 - Eliminar cuenta");
				Console.WriteLine("3 - Listar clientes con más de una cuenta");
				Console.WriteLine("4 - Realizar extracción");
				Console.WriteLine("5 - Depositar dinero");
				Console.WriteLine("6 - Transferir dinero entre cuentas");
				Console.WriteLine("7 - Listar cuentas");
				Console.WriteLine("8 - Listar clientes");
				Console.WriteLine("9 - Listar cuentas (recursivo)");
				Console.WriteLine("0 - Salir");
				Console.Write("Ingrese una opción: ");
				
				//verificamos si existe errores en el valor ingresado con exceptions
				try{
					//obtener dato ingresado por el usuario
					string elegido = Console.ReadLine();
					
					if(elegido.Length == 1 && elegido[0] >= '0' && elegido[0] <= '9'){ // <-- CORREGIDO (0 a 9)
						opcion = Convert.ToInt32(elegido);
					}else{
						//creo una exception si los valores no son ningunos de los esperados
						string mensaje_error = string.Format("El valor seleccionado: {0}. No es correcto. Elija una opción entre 0 y 9", elegido); // <-- CORREGIDO (decía 0 y 8)
						throw new FormatException(mensaje_error);
					}
				}catch(FormatException ex){
					//limpio la consola 
					Console.Clear();
					//aca para verificar que sea un int
					Console.WriteLine("Error: {0}",ex.Message);
					opcion = -1; // <--- CORREGIDO: Asegura que el bucle no termine si se ingresa texto
					
				}catch(Exception){
					Console.Write("Hubo un error");
					opcion = -1; // <--- CORREGIDO: Asegura que el bucle no termine
				}
				
				//despues de pasar la validación seguimos con el switch case
				//pasar la variable elegida a un switch 
				
				
			
				// === CORREGIDO: ESTE BLOQUE SE ELIMINÓ PORQUE PEDÍA LA OPCIÓN POR SEGUNDA VEZ ===
				/*
				//------------------DISTINTAS OPCIONES----------------------------
				string entrada = Console.ReadLine();
				if(int.TryParse(entrada, out opcion) == false){
					opcion = -1;
				}
				*/
				// ===============================================================================
				
				Console.WriteLine();
				
				if (opcion == 1){
					AgregarCuenta(banco);
				
				}
				else if (opcion == 2){
					EliminarCuenta(banco);
				}
				
				else if (opcion == 3){
					ListarClientesConVariasCuentas(banco);
				}
				
				else if (opcion == 4){
					RealizarExtraccion(banco);
				}
				
				
				else if (opcion == 5){
					DepositarDinero(banco);
				}
				
				else if (opcion == 6){
					TransferirDinero(banco);
					
				}
				
				else if (opcion == 7){
					ListarCuentas(banco);
				}
				
				else if (opcion == 8){
					ListarClientes(banco);
					
				}
				
				else if (opcion == 9)
				{
					ListarCuentasRecursivas(banco);
				}
				
				else if (opcion == 0){
					Console.WriteLine("Saliendo del sistema...");
				}
				
				else{
					Console.WriteLine("Opción inválida.");
				}
				
				if (opcion != 0){
					Console.WriteLine("Presione una tecla para continuar...");
					Console.ReadKey();
				}
				
			} while (opcion != 0);
			
			
		}
		
		
	}
}
