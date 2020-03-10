using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROYECTOclub
{
    class validaciones
    {
        public static void soloNumeros(KeyPressEventArgs pe)
        {
            if (char.IsDigit(pe.KeyChar))
            {
                pe.Handled = false;
            }
            else if (char.IsControl(pe.KeyChar))
            {
                pe.Handled = false;
            }
            else
            {
                pe.Handled = true;
            }

        }

        public static void Sololetras(KeyPressEventArgs pe)
        {
            if (char.IsLetter(pe.KeyChar))
            {
                pe.Handled = false;
            }
            else if (Char.IsControl(pe.KeyChar))
            {
                pe.Handled = false;
            }
            else if (Char.IsSeparator(pe.KeyChar))
            {
                pe.Handled = false;
            }
            else
            {
                pe.Handled = true;
            }
        }


        public static bool ValidarCorreo(string email)
        {
            String exp1;
            exp1 = (@"\A(\w+\.?\w*\@(gmail|hotmail|yahoo|outlook|itz)\.)(com)\Z");

            if (Regex.IsMatch(email, exp1))
            {
                if (Regex.Replace(email, exp1, String.Empty).Length == 0)
                {
                    return true;
                }
                else
                {

                    MessageBox.Show("El formato del correo ingresado no es correcto", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                    
                }
            }
            else
            {
                MessageBox.Show("El formato del correo ingresado no es correcto", "Validacion de campos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
    }
}
