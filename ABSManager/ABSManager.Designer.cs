
namespace ABSManager
{
    partial class frmManager
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManager));
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.scFunction = new System.Windows.Forms.SplitContainer();
            this.cmdDelete = new System.Windows.Forms.Button();
            this.cmdNew = new System.Windows.Forms.Button();
            this.scContent = new System.Windows.Forms.SplitContainer();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.txtKeyword = new System.Windows.Forms.TextBox();
            this.scTask = new System.Windows.Forms.SplitContainer();
            this.cmdFileBackup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scFunction)).BeginInit();
            this.scFunction.Panel1.SuspendLayout();
            this.scFunction.Panel2.SuspendLayout();
            this.scFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scContent)).BeginInit();
            this.scContent.Panel1.SuspendLayout();
            this.scContent.Panel2.SuspendLayout();
            this.scContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scTask)).BeginInit();
            this.scTask.SuspendLayout();
            this.SuspendLayout();
            // 
            // scMain
            // 
            this.scMain.BackColor = System.Drawing.Color.White;
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(0, 0);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.scFunction);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scContent);
            this.scMain.Size = new System.Drawing.Size(784, 561);
            this.scMain.SplitterDistance = 189;
            this.scMain.TabIndex = 0;
            // 
            // scFunction
            // 
            this.scFunction.BackColor = System.Drawing.Color.White;
            this.scFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scFunction.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scFunction.Location = new System.Drawing.Point(0, 0);
            this.scFunction.Name = "scFunction";
            this.scFunction.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scFunction.Panel1
            // 
            this.scFunction.Panel1.BackColor = System.Drawing.Color.White;
            this.scFunction.Panel1.Controls.Add(this.cmdDelete);
            this.scFunction.Panel1.Controls.Add(this.cmdNew);
            // 
            // scFunction.Panel2
            // 
            this.scFunction.Panel2.BackColor = System.Drawing.Color.White;
            this.scFunction.Panel2.Controls.Add(this.cmdFileBackup);
            this.scFunction.Size = new System.Drawing.Size(189, 561);
            this.scFunction.SplitterDistance = 44;
            this.scFunction.TabIndex = 0;
            // 
            // cmdDelete
            // 
            this.cmdDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(147)))), ((int)(((byte)(231)))));
            this.cmdDelete.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.cmdDelete.FlatAppearance.BorderSize = 0;
            this.cmdDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdDelete.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdDelete.ForeColor = System.Drawing.Color.White;
            this.cmdDelete.Location = new System.Drawing.Point(101, 10);
            this.cmdDelete.Name = "cmdDelete";
            this.cmdDelete.Size = new System.Drawing.Size(75, 25);
            this.cmdDelete.TabIndex = 1;
            this.cmdDelete.Text = "删除";
            this.cmdDelete.UseVisualStyleBackColor = false;
            // 
            // cmdNew
            // 
            this.cmdNew.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(147)))), ((int)(((byte)(231)))));
            this.cmdNew.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.cmdNew.FlatAppearance.BorderSize = 0;
            this.cmdNew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdNew.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdNew.ForeColor = System.Drawing.Color.White;
            this.cmdNew.Location = new System.Drawing.Point(15, 10);
            this.cmdNew.Name = "cmdNew";
            this.cmdNew.Size = new System.Drawing.Size(75, 25);
            this.cmdNew.TabIndex = 0;
            this.cmdNew.Text = "新增";
            this.cmdNew.UseVisualStyleBackColor = false;
            // 
            // scContent
            // 
            this.scContent.BackColor = System.Drawing.Color.White;
            this.scContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scContent.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scContent.Location = new System.Drawing.Point(0, 0);
            this.scContent.Name = "scContent";
            this.scContent.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scContent.Panel1
            // 
            this.scContent.Panel1.BackColor = System.Drawing.Color.White;
            this.scContent.Panel1.Controls.Add(this.cmdSearch);
            this.scContent.Panel1.Controls.Add(this.txtKeyword);
            // 
            // scContent.Panel2
            // 
            this.scContent.Panel2.Controls.Add(this.scTask);
            this.scContent.Size = new System.Drawing.Size(591, 561);
            this.scContent.SplitterDistance = 44;
            this.scContent.TabIndex = 0;
            // 
            // cmdSearch
            // 
            this.cmdSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(122)))), ((int)(((byte)(147)))), ((int)(((byte)(231)))));
            this.cmdSearch.FlatAppearance.BorderColor = System.Drawing.Color.DarkGray;
            this.cmdSearch.FlatAppearance.BorderSize = 0;
            this.cmdSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdSearch.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdSearch.ForeColor = System.Drawing.Color.White;
            this.cmdSearch.Location = new System.Drawing.Point(352, 10);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(75, 25);
            this.cmdSearch.TabIndex = 1;
            this.cmdSearch.Text = "搜索";
            this.cmdSearch.UseVisualStyleBackColor = false;
            // 
            // txtKeyword
            // 
            this.txtKeyword.AllowDrop = true;
            this.txtKeyword.BackColor = System.Drawing.Color.White;
            this.txtKeyword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeyword.Font = new System.Drawing.Font("Microsoft JhengHei", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtKeyword.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtKeyword.Location = new System.Drawing.Point(6, 10);
            this.txtKeyword.Name = "txtKeyword";
            this.txtKeyword.Size = new System.Drawing.Size(346, 25);
            this.txtKeyword.TabIndex = 0;
            // 
            // scTask
            // 
            this.scTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTask.Location = new System.Drawing.Point(0, 0);
            this.scTask.Name = "scTask";
            this.scTask.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scTask.Panel1
            // 
            this.scTask.Panel1.BackColor = System.Drawing.Color.White;
            // 
            // scTask.Panel2
            // 
            this.scTask.Panel2.BackColor = System.Drawing.Color.White;
            this.scTask.Size = new System.Drawing.Size(591, 513);
            this.scTask.SplitterDistance = 199;
            this.scTask.TabIndex = 0;
            // 
            // cmdFileBackup
            // 
            this.cmdFileBackup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("cmdFileBackup.BackgroundImage")));
            this.cmdFileBackup.FlatAppearance.BorderSize = 0;
            this.cmdFileBackup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdFileBackup.Location = new System.Drawing.Point(4, 4);
            this.cmdFileBackup.Name = "cmdFileBackup";
            this.cmdFileBackup.Size = new System.Drawing.Size(186, 40);
            this.cmdFileBackup.TabIndex = 0;
            this.cmdFileBackup.UseVisualStyleBackColor = true;
            // 
            // frmManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.scMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.Name = "frmManager";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ABS任务管理器";
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.scFunction.Panel1.ResumeLayout(false);
            this.scFunction.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scFunction)).EndInit();
            this.scFunction.ResumeLayout(false);
            this.scContent.Panel1.ResumeLayout(false);
            this.scContent.Panel1.PerformLayout();
            this.scContent.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scContent)).EndInit();
            this.scContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scTask)).EndInit();
            this.scTask.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scMain;
        private System.Windows.Forms.SplitContainer scFunction;
        private System.Windows.Forms.Button cmdDelete;
        private System.Windows.Forms.Button cmdNew;
        private System.Windows.Forms.SplitContainer scContent;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.TextBox txtKeyword;
        private System.Windows.Forms.SplitContainer scTask;
        private System.Windows.Forms.Button cmdFileBackup;
    }
}

