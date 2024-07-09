using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Payroll_Frontend.Models;
using System;
using System.Threading.Tasks;

namespace Payroll_Frontend.Pages.Section
{
    public class IngresosPageModel : PageModel
    {
        private readonly ApiService _apiService;
        public string baseUrl = "https://localhost:44326/api/IncomeTypes";
        public List<IncomeTypes> IncomeTypes { get; set; }
        public IncomeTypes Employee { get; set; } // Propiedad para almacenar el empleado obtenido por ID

        public IngresosPageModel(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task OnGetAsync()
        {
            try
            {
                IncomeTypes = await _apiService.GetDataAsync<List<IncomeTypes>>(baseUrl);
            }
            catch (Exception ex)
            {
                // Manejar errores de manera adecuada, por ejemplo, registrar el error o mostrar un mensaje al usuario
                IncomeTypes = new List<IncomeTypes>(); // Inicializar la lista para evitar problemas en la vista
            }
        }

        public async Task<IActionResult> OnGetEmpleadoAsync(int id)
        {
            try
            {
                Employee = await _apiService.GetDataByIdAsync<IncomeTypes>($"{baseUrl}/{id}", id.ToString());

                if (Employee == null)
                {
                    return NotFound();
                }

                return Page();
            }
            catch (Exception ex)
            {
                // Manejar otros tipos de errores
                return Page();
            }
        }

        public async Task<IActionResult> OnPostCrearAsync(IncomeTypes nuevoEmpleado)
        {
            try
            {
                HttpResponseMessage response = await _apiService.PostDataAsync(baseUrl, nuevoEmpleado);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Section/Empleados");
                }
                else
                {
                    // Manejar error si la creación no fue exitosa
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Manejar otros tipos de errores
                return Page();
            }
        }

        public async Task<IActionResult> OnPostActualizarAsync(IncomeTypes empleadoActualizado)
        {
            try
            {
                HttpResponseMessage response = await _apiService.PutDataAsync($"{baseUrl}/{empleadoActualizado.Id}", empleadoActualizado);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Section/Empleados");

                }
                else
                {
                    // Manejar error si la actualización no fue exitosa
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Manejar otros tipos de errores
                return Page();
            }
        }

        public async Task<IActionResult> OnPostEliminarAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await _apiService.DeleteDataAsync($"{baseUrl}/{id}");

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("/Section/Empleados");

                }
                else
                {
                    // Manejar error si la eliminación no fue exitosa
                    return RedirectToPage("/Index");

                }
            }
            catch (Exception ex)
            {
                // Manejar otros tipos de errores
                return Page();
            }
        }
    }
}
