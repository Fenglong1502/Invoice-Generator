using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace InvoiceGenerator
{
    public partial class _Default : Page
    {
        ArrayList purchasedOrder = new ArrayList();

        protected void Page_Load(object sender, EventArgs e)
        {

            flushData();
        }

        public iTextSharp.text.Font getBigFontSize()
        {
            var fontName = "Times";
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 18);
        }

        public iTextSharp.text.Font getInvoiceTextFontSize()
        {
            var fontName = "Times";
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 12);
        }

        public iTextSharp.text.Font getNormalFontSize()
        {
            var fontName = "Times";
            return FontFactory.GetFont(fontName, BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10);
        }


        //// Import Demo Data
        //// User should import these data from purchase order database
        public void flushData()
        {
            purchasedOrder.Add(new PurchasedItem("Art of Thinking Clearly: Better Thinking, Better Decisions", 1 ,19.90));
            purchasedOrder.Add(new PurchasedItem("Rubik's Cube 3X3", 1 ,9.90));
            purchasedOrder.Add(new PurchasedItem("Apple Iphone Xs Max 64GB", 1 ,1799));
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            /* 
            *  Generate PDF - Creating Invoice Details
            *  Fake data - Invoice Num, Invoice Date and Description
            */
            string invoiceNum = invoiceNumTB.Text;
            string date = DateTime.Now.ToString("dd-MMMM-yyyy");
            string description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore";

            Document doc = new Document(iTextSharp.text.PageSize.LETTER, 10, 10, 42, 35);
            string path = Server.MapPath("Invoices");
            PdfWriter wri = PdfWriter.GetInstance(doc, new FileStream(path + "/" + invoiceNum + ".pdf", FileMode.Create));
            doc.Open();

            /*  
            *  Creating Invoice - Insert Header (Logo at the top left & Billing Address at the Right) 
            */
            PdfPTable header = new PdfPTable(3);
            header.TotalWidth = 550f;
            header.LockedWidth = true;
            float[] headerWidths = new float[] { 2f, 1f, 2f };
            header.SetWidths(headerWidths);
            header.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;

            string imagePath = Server.MapPath("images");
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath + "/logo1.png");
            logo.ScalePercent(70);

            PdfPCell logoCell = new PdfPCell(logo, false);
            logoCell.Border = 0;
            logoCell.VerticalAlignment = Element.ALIGN_MIDDLE;
            logoCell.HorizontalAlignment = Element.ALIGN_CENTER;
            header.AddCell(logoCell);

            Paragraph emptySpace = new Paragraph(" ");
            header.AddCell(emptySpace);


            PdfPTable addressTable = new PdfPTable(1);
            addressTable.TotalWidth = 220f;
            addressTable.LockedWidth = true;
            PdfPCell companyName = new PdfPCell(new Phrase("ABC Company Pte Ltd", getBigFontSize()));
            PdfPCell companyStreet = new PdfPCell(new Phrase("1234 SomeStreet", getNormalFontSize()));
            PdfPCell companyAddress = new PdfPCell(new Phrase("Anywhere USA 21212", getNormalFontSize()));
            companyName.Border = 0;
            companyStreet.Border = 0;
            companyAddress.Border = 0;
            addressTable.AddCell(companyName);
            addressTable.AddCell(companyStreet);
            addressTable.AddCell(companyAddress);
            addressTable.DefaultCell.Border = iTextSharp.text.Rectangle.NO_BORDER;
            header.AddCell(addressTable);

            header.AddCell(emptySpace);

            iTextSharp.text.Font whiteFont = getInvoiceTextFontSize();
            whiteFont.SetColor(255,255,255);
            PdfPCell invoiceHeader = new PdfPCell(new Phrase("Invoice", whiteFont));
            invoiceHeader.Border = 0;
            invoiceHeader.HorizontalAlignment = Element.ALIGN_CENTER;
            invoiceHeader.VerticalAlignment = Element.ALIGN_TOP;
            invoiceHeader.BackgroundColor = new iTextSharp.text.BaseColor(102, 180, 51);

            header.AddCell(invoiceHeader);
            header.AddCell(emptySpace);

            doc.Add(header);

            /*  
            *  Creating Invoice - Display Invoice information
            */ 
            PdfPTable paragraph = new PdfPTable(1);
            paragraph.TotalWidth = 550f;
            paragraph.LockedWidth = true;

            PdfPCell invoiceNoCell = new PdfPCell(new Phrase("Invoice Number: " + invoiceNum));
            invoiceNoCell.BackgroundColor = new iTextSharp.text.BaseColor(222, 223, 222);

            invoiceNoCell.Border = 0;
            paragraph.AddCell(invoiceNoCell);

            PdfPCell invoiceDateCell = new PdfPCell(new Phrase("Date: " + date));
            invoiceDateCell.BackgroundColor = new iTextSharp.text.BaseColor(222, 223, 222);
            invoiceDateCell.Border = 0;
            paragraph.AddCell(invoiceDateCell);

            PdfPCell invoiceDescriptionCell = new PdfPCell(new Phrase("\nDescription: \n" + description));
            invoiceDescriptionCell.BackgroundColor = new iTextSharp.text.BaseColor(222, 223, 222);
            invoiceDescriptionCell.Border = 0;
            paragraph.AddCell(invoiceDescriptionCell);

            doc.Add(paragraph);
            doc.Add(emptySpace);    //Enter Spaace at the bottom


            /*  
            *  Creating Invoice - Purchase Order Items 
            *  For demo purposes, i have used ArrayList to store and display demo data. 
            *  Data can also be displayed from Database
            */  
            PdfPTable table = new PdfPTable(4);
            table.TotalWidth = 550f;
            table.LockedWidth = true;
            float[] widths = new float[] { 6f, 1f, 1f, 1f };
            table.SetWidths(widths);

            PdfPCell cell1 = new PdfPCell(new Phrase("Product Name"));
            cell1.BackgroundColor = new iTextSharp.text.BaseColor(222, 223, 222);
            table.AddCell(cell1);
            PdfPCell cell2 = new PdfPCell(new Phrase("Unit Price"));
            cell2.BackgroundColor = new iTextSharp.text.BaseColor(222, 223, 222);
            table.AddCell(cell2);
            PdfPCell cell3 = new PdfPCell(new Phrase("Quantiy"));
            cell3.BackgroundColor = new iTextSharp.text.BaseColor(222, 223, 222);
            table.AddCell(cell3);
            PdfPCell cell4 = new PdfPCell(new Phrase("Sub-Total"));
            cell4.BackgroundColor = new iTextSharp.text.BaseColor(222, 223, 222);
            table.AddCell(cell4);

            double totalCost = 0;
            foreach (PurchasedItem pi in purchasedOrder)
            {
                double totalUnitPrice = Math.Round(pi.Qt * pi.UnitPrice, 2);
                totalCost += totalUnitPrice;
                table.AddCell(pi.ProductName);
                table.AddCell("$" + pi.UnitPrice.ToString("F"));
                table.AddCell(pi.Qt.ToString());
                table.AddCell("$" + totalUnitPrice.ToString("F"));
            }

            PdfPCell cellTotalDescription = new PdfPCell(new Phrase("Total Price"));
            cellTotalDescription.Colspan = 3;
            cellTotalDescription.BackgroundColor = new iTextSharp.text.BaseColor(222, 223, 222);
            cellTotalDescription.PaddingRight = 1;
            table.AddCell(cellTotalDescription);

            PdfPCell cellTotal = new PdfPCell(new Phrase("$" + totalCost.ToString("F")));
            cellTotal.BackgroundColor = new iTextSharp.text.BaseColor(222, 223, 222);   
            table.AddCell(cellTotal);

            doc.Add(table);
            doc.Add(emptySpace);
            doc.Add(emptySpace);

            /*  
            *  Creating Invoice - Display Invoice Footer
            */
            string currentDate = System.DateTime.Today.ToShortDateString();
            Paragraph footer = new Paragraph("PDF Generated on: " + currentDate);
            doc.Add(footer);
            doc.Close();

            /*  
            *  // Sending Invoice PDF to Customer's Email
            *  string email = "CustomerEmail@test.com"; //Attach customer email address
            *  string fileName = path + "/" + invoiceNum + ".pdf"; // attach the invoice number
            *  SendEmail(email, fileName); // SendEmail Function
            */

            Response.Write("<script>alert('" + invoiceNumTB.Text + ".pdf has been generated in Invoices folder in the working directory!');</script>");
        }

        protected static void SendEmail(string email, string attachName)
        {
            string subject = "Invoice PDF";
            string body = "The current attachment is the invoice of your purchase order. Thank you for shopping with us.";
            using (System.Net.Mail.MailMessage mm = new System.Net.Mail.MailMessage("company@gmail.com", email)) // Company email addres , Email to send 
            {
                mm.Subject = subject;
                mm.Body = body;
                mm.Attachments.Add(new Attachment(attachName));

                mm.IsBodyHtml = false;
                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 465);

                smtp.EnableSsl = true;
                NetworkCredential NetworkCred = new NetworkCredential("company@gmail.com", "password"); //Company email smtp email address and password 
                smtp.UseDefaultCredentials = true;
                smtp.Credentials = NetworkCred;
                smtp.Port = 587;
                smtp.Send(mm);

            }
        }

    }
}