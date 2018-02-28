using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Telerik.Web.UI;
using E_Utilities;
using System.Windows.Forms;

/// <summary>
/// Descripción breve de validarNyL
/// </summary>
public class validarNyL 
{
    Ejecuciones ejecuta = new Ejecuciones();

    public validarNyL()
    {


    }
    public void soloNumeros(object sender, KeyPressEventArgs e)
    {


        if (char.IsDigit(e.KeyChar))
        {
            e.Handled = false;
        }
        else if (char.IsControl(e.KeyChar))
        {
            e.Handled = false;
        }
        else
        {
            e.Handled = true;
        }


    }
}