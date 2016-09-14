namespace Matrix
{
   partial class SaveForm
   {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing)
      {
         if (disposing && (components != null))
         {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent()
      {
         this.tb_save = new System.Windows.Forms.TextBox();
         this.btn_save = new System.Windows.Forms.Button();
         this.SuspendLayout();
         // 
         // tb_save
         // 
         this.tb_save.Location = new System.Drawing.Point(8, 12);
         this.tb_save.Name = "tb_save";
         this.tb_save.Size = new System.Drawing.Size(100, 20);
         this.tb_save.TabIndex = 0;
         this.tb_save.TextChanged += new System.EventHandler(this.tb_save_TextChanged);
         // 
         // btn_save
         // 
         this.btn_save.Location = new System.Drawing.Point(23, 38);
         this.btn_save.Name = "btn_save";
         this.btn_save.Size = new System.Drawing.Size(75, 23);
         this.btn_save.TabIndex = 1;
         this.btn_save.Text = "Save";
         this.btn_save.UseVisualStyleBackColor = true;
         this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
         // 
         // SaveForm
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(116, 67);
         this.Controls.Add(this.btn_save);
         this.Controls.Add(this.tb_save);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "SaveForm";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
         this.Text = "Saver";
         this.Load += new System.EventHandler(this.SaveForm_Load);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.TextBox tb_save;
      private System.Windows.Forms.Button btn_save;
   }
}