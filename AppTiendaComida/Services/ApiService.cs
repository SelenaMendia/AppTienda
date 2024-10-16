using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using AppTiendaComida.Models;
using System.Security.Cryptography;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Globalization;
using AppTiendaComida.Models.DTO;




namespace AppTiendaComida.Services
{
    public class ApiService
    {
        private static readonly string BASE_URL = "https://localhost:7042/api/";
        static HttpClient httpClient = new HttpClient() { Timeout = TimeSpan.FromSeconds(60) };
       
        //Login
        public async Task<LoginResponseDto> ValidarLogin(string _correo, string _contraseña)
        {
            string FINAL_URL = BASE_URL + "Usuario/ValidarCredencial";
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(
                        new
                        {
                            Email = _correo,
                            Password = _contraseña,
                            //password = Encriptar.GetSHA256(_contraseña),
                        }),
                    Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

                
                if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new Exception("Error de credenciales. Vuelve a ingresar.");
                }

                
                if (!result.IsSuccessStatusCode)
                {
                    throw new Exception("Ocurrió un error inesperado.");
                }

                var jsonData = await result.Content.ReadAsStringAsync();
                if (!string.IsNullOrWhiteSpace(jsonData))
                {
                    var responseObject = JsonSerializer.Deserialize<LoginResponseDto>(jsonData,
                        new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        });
                    return responseObject!;
                }
                else
                {
                    throw new Exception("Error al recibir los datos.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //lista de productos
        public static async Task<List<Producto>> GetProductos()
        {
            string FINAL_URL = BASE_URL + "Producto/ObtenerLista";

            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Inside the ApiService class
                        var responseObject = JsonSerializer.Deserialize<List<Producto>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        Exception exception = new Exception("Resource Not Found");
                        throw new Exception(exception.Message);
                    }
                }
                else
                {
                    Exception exception = new Exception("Request failed with status code " + response.StatusCode);
                    throw new Exception(exception.Message);
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }


        //ver un producto por id

        public static async Task<Producto> GetProductoPorId(int productoId)
        {
            string FINAL_URL = BASE_URL + $"Producto/ObtenerPorId/{productoId}"; // Asegúrate que tu API soporte esta ruta.

            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        var responseObject = JsonSerializer.Deserialize<Producto>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        throw new Exception("Producto no encontrado.");
                    }
                }
                else
                {
                    throw new Exception($"La solicitud falló con el código de estado {response.StatusCode}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        //public static async Task<bool> AgregarProducto(Producto _producto)
        //{
        //    string FINAL_URL = BASE_URL + "Producto";
        //    try
        //    {
        //        var content = new StringContent(
        //                JsonSerializer.Serialize(_producto),
        //                Encoding.UTF8, "application/json"
        //            );

        //        var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);



        //        if (result.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        //public static async Task<bool> AgregarProducto(Producto _producto, byte[] imagen)
        //{
        //    string FINAL_URL = BASE_URL + "Producto/CrearConImagen";

        //    // Validar que la imagen no sea nula o esté vacía
        //    if (imagen == null || imagen.Length == 0)
        //    {
        //        throw new ArgumentException("La imagen es obligatoria.");
        //    }

        //    try
        //    {
        //        using (var content = new MultipartFormDataContent())
        //        {
        //            var productoJson = JsonSerializer.Serialize(_producto);
        //            Console.WriteLine(productoJson); // Imprimir JSON para verificar
        //            content.Add(new StringContent(productoJson, Encoding.UTF8, "application/json"), "producto");

        //            string imagenNombre = _producto.Nombre.Replace(" ", "_") + ".png";
        //            var imagenContent = new ByteArrayContent(imagen);
        //            imagenContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/png");
        //            content.Add(imagenContent, "imagen", imagenNombre);

        //            var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

        //            // Inspeccionar respuesta
        //            if (!result.IsSuccessStatusCode)
        //            {
        //                var errorResponse = await result.Content.ReadAsStringAsync();
        //                throw new Exception($"Error al agregar producto: {result.StatusCode} - {errorResponse}");
        //            }

        //            return true; // Producto agregado exitosamente
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al agregar producto: " + ex.Message);
        //    }
        //}
        //public static async Task<bool> AgregarProducto(Producto _Producto, byte[] imagen)
        //{
        //    string FINAL_URL = BASE_URL + "Producto/CrearConImagen";

        //    try
        //    {
        //        using (var form = new MultipartFormDataContent())
        //        {
        //            // Serializa el objeto Producto a JSON
        //            var jsonProducto = JsonSerializer.Serialize(_Producto);
        //            var stringContent = new StringContent(jsonProducto, Encoding.UTF8, "application/json");
        //            form.Add(stringContent, "producto");

        //            // Verifica si se proporciona un archivo de imagen
        //            if (imagen != null && imagen.Length > 0)
        //            {
        //                var fileContent = new ByteArrayContent(imagen);
        //                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg"); // Cambia según el tipo de imagen
        //                form.Add(fileContent, "imagen", "imagen.jpg"); // Cambia el nombre del archivo según sea necesario
        //            }

        //            // Envía la solicitud
        //            var result = await httpClient.PostAsync(FINAL_URL, form).ConfigureAwait(false);

        //            // Verifica si la respuesta fue exitosa
        //            return result.StatusCode == System.Net.HttpStatusCode.OK;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al agregar producto: " + ex.Message);
        //    }
        //}

        //public static async Task<bool> AgregarProducto(CrearProductoDto productoDto)
        //{
        //    string FINAL_URL = BASE_URL + "Producto/CrearConImagen";

        //    try
        //    {



        //        using (var form = new MultipartFormDataContent())
        //        {
        //            // Serializando el objeto CrearProductoDto a JSON
        //            var jsonProducto = JsonSerializer.Serialize(productoDto);
        //            var stringContent = new StringContent(jsonProducto, Encoding.UTF8, "application/json");

        //            // Agregar el contenido JSON al formulario
        //            form.Add(stringContent, "producto"); // Suponiendo que la API espera el JSON bajo la clave "producto"

        //            // Agregar la imagen
        //            if (productoDto.Imagen != null)
        //            {
        //                var streamContent = new StreamContent(productoDto.Imagen.OpenReadStream());
        //                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(productoDto.Imagen.ContentType);
        //                form.Add(streamContent, "imagen", productoDto.Imagen.FileName); // Agregar la imagen con la clave "imagen"
        //            }

        //            // Enviar la solicitud
        //            var result = await httpClient.PostAsync(FINAL_URL, form).ConfigureAwait(false);

        //            // Verificar si la respuesta fue exitosa
        //            return result.StatusCode == System.Net.HttpStatusCode.OK;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error al agregar producto: " + ex.Message);
        //    }
        //}

        //public static async Task<bool> PostProductosAsync(CrearProductoDto producto)
        //{
        //    string FINAL_URL = BASE_URL + "Producto/CrearConImagen";

        //    var content = new MultipartFormDataContent();

        //    // Agregar el resto de las propiedades del producto
        //    content.Add(new StringContent(producto.Nombre ?? ""), "Nombre");
        //    content.Add(new StringContent(producto.Precio.ToString()), "Precio");
        //    content.Add(new StringContent(producto.Descripcion ?? ""), "Descripcion");
        //    //content.Add(new StringContent(producto.CategoriaId.ToString()), "category");
        //    content.Add(new StringContent(producto.Stock.ToString()), "Stock");

        //    // Agregar la imagen
        //    //if (producto.Imagen != null)
        //    //{
        //    //    var streamContent = new StreamContent(producto.Imagen.OpenReadStream());
        //    //    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(producto.Imagen.ContentType);
        //    //    content.Add(streamContent, "imagen", producto.Imagen.FileName); // Agregar la imagen con la clave "imagen"
        //    //}
        //    //if (producto.Imagen != null)
        //    //{
        //    //    using var stream = producto.Imagen.OpenReadStream();
        //    //    var streamContent = new StreamContent(stream);
        //    //    streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(producto.Imagen.ContentType);

        //    //    // Adjuntar la imagen al form-data con el nombre del campo "imagen"
        //    //    content.Add(streamContent, "Imagen", producto.Imagen.FileName);
        //    //}

        //    if (producto.Imagen != null)
        //    {
        //        var stream = await producto.Imagen.OpenReadAsync();
        //        var fileContent = new StreamContent(stream);
        //        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg"); // o el tipo adecuado
        //        content.Add(fileContent, "Imagen", producto.Imagen.FileName);
        //    }
        //    var response = await httpClient.PostAsync(FINAL_URL, content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Deserializar la respuesta si es exitosa
        //        return true;
        //    }
        //    else
        //    {
        //        // Manejar errores de la solicitud
        //        throw new HttpRequestException($"Error en la solicitud: {response.StatusCode}");
        //    }
        //}

        //public static async Task<bool> PostProductosAsync(CrearProductoDto producto)
        //{
        //    string FINAL_URL = BASE_URL + "Producto/CrearConImagen";

        //    var content = new MultipartFormDataContent();

        //    // Agregar el resto de las propiedades del producto
        //    content.Add(new StringContent(producto.Nombre ?? ""), "Nombre");
        //    content.Add(new StringContent(producto.Precio.ToString()), "Precio");
        //    content.Add(new StringContent(producto.Descripcion ?? ""), "Descripcion");
        //    content.Add(new StringContent(producto.Stock.ToString()), "Stock");



        //        using var stream = producto.Imagen.OpenReadStream();  // Cambiado de OpenReadAsync() a OpenReadStream()
        //        var streamContent = new StreamContent(stream);
        //        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/jpeg");  // Ajusta según el tipo de imagen

        //        // Adjuntar la imagen al form-data con el nombre del campo "Imagen"
        //        content.Add(streamContent, "Imagen", producto.Imagen.FileName);






        //    var response = await httpClient.PostAsync(FINAL_URL, content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Deserializar la respuesta si es exitosa
        //        return true;
        //    }
        //    else
        //    {
        //        var errorMessage = await response.Content.ReadAsStringAsync(); // Leer el contenido de error
        //        throw new HttpRequestException($"Error en la solicitud: {response.StatusCode} - {errorMessage}");
        //    }
        //}

        //public static async Task<bool> PostProductosAsync(CrearProductoDto producto)
        //{
        //    string FINAL_URL = BASE_URL + "Producto/CrearConImagen";

        //    var content = new MultipartFormDataContent();

        //    // Agregar el resto de las propiedades del producto
        //    content.Add(new StringContent(producto.Nombre ?? ""), "Nombre");
        //    content.Add(new StringContent(producto.Precio.ToString()), "Precio");
        //    content.Add(new StringContent(producto.Descripcion ?? ""), "Descripcion");
        //    content.Add(new StringContent(producto.Stock.ToString()), "Stock");

        //    // Verifica si hay una imagen antes de procesar el stream
        //    if (producto.Imagen != null)
        //    {
        //        using var stream = producto.Imagen.OpenReadStream();  // Cambiado de OpenReadAsync() a OpenReadStream()
        //        var streamContent = new StreamContent(stream);

        //        // Verificar que el ContentType no sea null o vacío y asignar un valor por defecto si es necesario
        //        var contentType = !string.IsNullOrEmpty(producto.Imagen.ContentType)
        //            ? producto.Imagen.ContentType
        //            : "application/octet-stream";  // Valor por defecto si ContentType es null

        //        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

        //        // Adjuntar la imagen al form-data con el nombre del campo "Imagen"
        //        content.Add(streamContent, "Imagen", producto.Imagen.FileName);
        //    }

        //    var response = await httpClient.PostAsync(FINAL_URL, content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Deserializar la respuesta si es exitosa
        //        return true;
        //    }
        //    else
        //    {
        //        var errorMessage = await response.Content.ReadAsStringAsync(); // Leer el contenido de error
        //        throw new HttpRequestException($"Error en la solicitud: {response.StatusCode} - {errorMessage}");
        //    }
        //}

        public static async Task<bool> PostProductosAsync(CrearProductoDto producto)
        {
            string FINAL_URL = BASE_URL + "Producto/CrearConImagen";
            var content = new MultipartFormDataContent();

            // Agregar el resto de las propiedades del producto
            content.Add(new StringContent(producto.Nombre ?? ""), "Nombre");
            content.Add(new StringContent(producto.Precio.ToString()), "Precio");
            content.Add(new StringContent(producto.Descripcion ?? ""), "Descripcion");
            content.Add(new StringContent(producto.Stock.ToString()), "Stock");

            // Verificar si la imagen no es nula
            if (producto.Imagen != null)
            {
                using var stream = producto.Imagen.OpenReadStream();
                var streamContent = new StreamContent(stream);

                // Verificar el ContentType y asignar un valor por defecto si es necesario
                var contentType = !string.IsNullOrEmpty(producto.Imagen.ContentType)
                    ? producto.Imagen.ContentType
                    : "application/octet-stream";  // Valor por defecto si el tipo no se detecta

                // Asignar el tipo de contenido
                streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);

                // Adjuntar la imagen al form-data con el nombre del campo "Imagen"
                content.Add(streamContent, "Imagen", producto.Imagen.FileName);
            }

            // Enviar la solicitud HTTP POST
            var response = await httpClient.PostAsync(FINAL_URL, content);

            if (response.IsSuccessStatusCode)
            {
                return true;  // Producto creado correctamente
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode} - {errorMessage}");
            }
            //esto debo cambiar el Task en Producto 
            //if (response.IsSuccessStatusCode)
            //{
            //    // Leer y devolver el objeto Producto creado
            //    var productoCreado = await response.Content.ReadFromJsonAsync<Producto>();
            //    return productoCreado;  // Devolver el producto creado
            //}
            //else
            //{
            //    var errorMessage = await response.Content.ReadAsStringAsync();
            //    throw new HttpRequestException($"Error en la solicitud: {response.StatusCode} - {errorMessage}");
            //}
        }



        //public static async Task<bool> PostProductosAsync(CrearProductoDto producto)
        //{
        //    string FINAL_URL = BASE_URL + "Producto/CrearConImagen";
        //    var content = new MultipartFormDataContent();

        //    // Agregar el resto de las propiedades del producto
        //    content.Add(new StringContent(producto.Nombre ?? ""), "Nombre");
        //    content.Add(new StringContent(producto.Precio.ToString()), "Precio");
        //    content.Add(new StringContent(producto.Descripcion ?? ""), "Descripcion");
        //    content.Add(new StringContent(producto.Stock.ToString()), "Stock");

        //    // Verifica si hay una imagen y procesa el stream
        //    if (producto.Imagen != null)
        //    {
        //        using var stream = await producto.Imagen.OpenReadAsync();
        //        var streamContent = new StreamContent(stream);
        //        streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(producto.Imagen.ContentType);  // Usar el tipo de contenido de la imagen

        //        // Adjuntar la imagen al form-data con el nombre del campo "Imagen"
        //        content.Add(streamContent, "Imagen", producto.Imagen.FileName);
        //    }

        //    var response = await httpClient.PostAsync(FINAL_URL, content);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        var errorMessage = await response.Content.ReadAsStringAsync(); // Leer el contenido de error
        //        throw new HttpRequestException($"Error en la solicitud: {response.StatusCode} - {errorMessage}");
        //    }
        //}




        public static async Task<bool> AgregarUsuario(Usuario _usuario)
        {
            string FINAL_URL = BASE_URL + "Usuario";
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(_usuario),
                    Encoding.UTF8, "application/json"
                );

                var result = await httpClient.PostAsync(FINAL_URL, content).ConfigureAwait(false);

                if (result.IsSuccessStatusCode) // Cambié aquí
                {
                    // Aquí puedes agregar lógica para obtener el ID o detalles del usuario creado
                    return true;
                }

                // Puedes leer el mensaje de error de la respuesta para diagnosticar
                var errorResponse = await result.Content.ReadAsStringAsync();
                throw new Exception($"Error al agregar usuario: {errorResponse}");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para obtener la lista de usuarios
        public async Task<List<Usuario>> GetUsuarios()
        {
            string FINAL_URL = BASE_URL + "Usuario/ObtenerLista";

            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.IsSuccessStatusCode)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        var responseObject = JsonSerializer.Deserialize<List<Usuario>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });
                        return responseObject!;
                    }
                    else
                    {
                        throw new Exception("Resource Not Found");
                    }
                }
                else
                {
                    throw new Exception("Request failed with status code " + response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Producto> PutProductosAsync(int productoId, CrearProductoDto producto)
        {

            string FINAL_URL = BASE_URL + $"Producto/Modificar/{productoId}";
            var content = new MultipartFormDataContent();

            // Asignar propiedades del producto
            content.Add(new StringContent(producto.Nombre ?? ""), "Nombre");
            content.Add(new StringContent(producto.Precio.ToString()), "Precio");
            content.Add(new StringContent(producto.Descripcion ?? ""), "Descripcion");
            content.Add(new StringContent(producto.Stock.ToString()), "Stock");

            // Manejo de la imagen
            if (producto.Imagen != null)
            {
                try
                {
                    var stream = producto.Imagen.OpenReadStream(); // Cambiar a OpenReadStream() si no es necesario await
                    var fileContent = new StreamContent(stream);

                    // Obtener el tipo de contenido de la imagen
                    var contentType = !string.IsNullOrEmpty(producto.Imagen.ContentType)
                        ? producto.Imagen.ContentType
                        : "application/octet-stream";  // Valor por defecto si no hay tipo de contenido

                    fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(contentType);
                    content.Add(fileContent, "Imagen", producto.Imagen.FileName);
                }
                catch (Exception ex)
                {
                    // Manejar el error al abrir el flujo de la imagen
                    throw new Exception("Error al abrir el flujo de la imagen: " + ex.Message);
                }
            }
            else
            {
                content.Add(new StringContent(string.Empty), "Imagen");
            }

            // Enviar la solicitud HTTP POST
            var response = await httpClient.PostAsync(FINAL_URL, content);

            if (response.IsSuccessStatusCode)
            {
                // Leer y devolver el objeto Producto creado
                var productoCreado = await response.Content.ReadFromJsonAsync<Producto>();
                return productoCreado;  // Devolver el producto creado
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode} - {errorMessage}");
            }
        }

        public async Task<Producto> SearchByIdAsync(int id)
        {
            string FINAL_URL = BASE_URL + $"Producto/ObtenerPorId/{id}"; // Asegúrate que tu API soporte esta ruta.

                var response = await httpClient.GetAsync(FINAL_URL);
            if (response.IsSuccessStatusCode)
            {
                // Leer y devolver el objeto Producto creado
                var productoCreado = await response.Content.ReadFromJsonAsync<Producto>();
                return productoCreado;  // Devolver el producto creado
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error en la solicitud: {response.StatusCode} - {errorMessage}");
            }
        }

        public static async Task<List<Usuario>> GetUsuario()
        {
            string FINAL_URL = BASE_URL + "Usuario/ObtenerLista";

            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        // Deserializar la respuesta JSON a una lista de UsuarioListaDTO
                        var responseObject = JsonSerializer.Deserialize<List<Usuario>>(jsonData,
                            new JsonSerializerOptions
                            {
                                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                                WriteIndented = true
                            });

                        return responseObject!; // Retornar la lista deserializada
                    }
                    else
                    {
                        // Lanzar excepción si no se encontró recurso
                        throw new Exception("Resource Not Found");
                    }
                }
                else
                {
                    // Lanzar excepción si la solicitud falla
                    throw new Exception("Request failed with status code " + response.StatusCode);
                }
            }
            catch (Exception exception)
            {
                // Lanzar excepción en caso de error
                throw new Exception(exception.Message);
            }
        }


        //public static async Task<Usuario> GetUsuarioPorId(int usuarioId)
        //{
        //    string FINAL_URL = BASE_URL + $"Usuario/ObtenerPorId/{usuarioId}"; // Asegúrate que tu API soporte esta ruta.

        //    try
        //    {
        //        var response = await httpClient.GetAsync(FINAL_URL);
        //        if (response.StatusCode == System.Net.HttpStatusCode.OK)
        //        {
        //            var jsonData = await response.Content.ReadAsStringAsync();
        //            if (!string.IsNullOrWhiteSpace(jsonData))
        //            {
        //                var responseObject = JsonSerializer.Deserialize<Usuario>(jsonData,
        //                    new JsonSerializerOptions
        //                    {
        //                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        //                        WriteIndented = true
        //                    });
        //                return responseObject!;
        //            }
        //            else
        //            {
        //                throw new Exception("Usuario no encontrado.");
        //            }
        //        }
        //        else
        //        {
        //            throw new Exception($"La solicitud falló con el código de estado {response.StatusCode}");
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        throw new Exception(exception.Message);
        //    }
        //}

        public static async Task<Usuario> GetUsuarioPorId(int usuarioId)
        {
            string FINAL_URL = BASE_URL + $"Usuario/ObtenerPorId/{usuarioId}";

            try
            {
                var response = await httpClient.GetAsync(FINAL_URL);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonData = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrWhiteSpace(jsonData))
                    {
                        var responseObject = JsonSerializer.Deserialize<Usuario>(jsonData, new JsonSerializerOptions
                        {
                            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                            WriteIndented = true
                        });
                        return responseObject;
                    }
                    else
                    {
                        throw new Exception("Usuario no encontrado.");
                    }
                }
                else
                {
                    throw new Exception($"La solicitud falló con el código de estado {response.StatusCode}");
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }





    }
}
