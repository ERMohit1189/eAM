<%@ Page Title="" Language="C#" MasterPageFile="~/11/comman_root_manager.master" AutoEventWireup="true" CodeFile="StudentRegViewNew.aspx.cs" Inherits="_11.AdminStudentRegView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderMainhead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderMainBox" runat="Server">

    <style>
        :root {
  --form-bg-color: #e8f0d8;
  --form-text-color: #333;
  --form-border-color: #666;
  --form-box-color: #fff;
}

* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

@media print {
    header{display: none !important;}
    .vd_head-section{display: none !important;}
    .vd_navbar-left {
        display: none !important;
    }
    .vd_content-wrapper .vd_container {
    margin-right:0px;
    margin-left: 0;
}
    .page-break {
        page-break-before: always; /* Ensures a new page starts */
        break-before: page; /* Modern alternative */
        padding-top:20px; /* Adds space before the break */
        padding-bottom:20px; /* Adds space after the break */
    }
    #app{display:none;}
}

.admission-form {
  max-width:1024px;
  margin: 0 auto;
  padding:10px;
  background-color: var(--form-bg-color);
  border: 1px solid var(--form-border-color);
}

.admission-form .header {
  display: flex;
  align-items: center;
  margin-bottom: 20px;
}

.admission-form .logo {
  flex: 0 0 80px;
}

.admission-form .school-logo {
  width: 100px;
  height: 100px;
  border-radius: 50%;
}

.admission-form .school-info {
  flex: 1;
  text-align: center;
}

.admission-form .since {
  font-size: 18px;
  font-weight: bold;
}

.admission-form h1 {
  font-size: 24px;
  margin: 5px 0;
  font-weight: bold;
  text-transform: uppercase;
}

.admission-form .location {
  font-size: 18px;
  margin-bottom: 5px;
  text-transform: uppercase;
}

.admission-form .address {
  font-size: 14px;
}

.admission-form .pioneer {
  flex: 0 0 150px;
  text-align: right;
}

.admission-form .pioneer-text {
  font-style: italic;
  font-size: 16px;
}

.admission-form .form-title {
  position: relative;
  text-align: center;
  margin:10px 0;
}
.admission-form .box2 {
    width: 30px;
    height: 24px;
    border: 1px solid var(--form-border-color);
    background-color: var(--form-box-color);
    margin-right: 2px;
}
.admission-form h2 {
  display: inline-block;
  border: 2px solid var(--form-border-color);
  padding: 5px 20px;
  font-size: 20px;
  text-transform: uppercase;
}

.admission-form .serial-number {
  position: absolute;
  right: 0;
  top: 50%;
  transform: translateY(-50%);
  font-weight: bold;
}

.admission-form .sl-no {
  font-weight: bold;
  font-size: 18px;
  color: #0000ff;
}

.admission-form .section-title {
  font-weight: bold;
  margin-bottom: 10px;
  text-transform: uppercase;
}

.admission-form .office-use {
  margin-bottom:0px;
  border: 1px solid var(--form-border-color);
  padding:6px;
}

.admission-form .field-row {
  display: flex;
  margin-bottom:6px;
  align-items: center;
}

.admission-form .field-label {
  flex: 0 0 124px;
  font-weight: normal;
  font-size: 12px;
  line-height:14px;
  white-space:nowrap;
}

.admission-form .field-input {
  flex: 1;
  height: 24px;
  background-color: var(--form-box-color);
  border:1px solid var(--form-border-color);
}

.admission-form .long-input {
  flex: 0 0 236px;
  max-width:250px;
}

.admission-form .boxes {
  display: flex;
  align-items:center;
}

.admission-form .box-row {
  display: flex;
  align-items:center;
}

.admission-form .box {
  width: 22px;
  min-width:22px;
  height: 24px;
  border: 1px solid var(--form-border-color);
  background-color: var(--form-box-color);
  margin-right: 2px;
}

.admission-form .instructions {
  margin: 6px 0;
  font-style: italic;
}

.admission-form .instruction-title {
  font-weight: bold;
  margin-bottom: 5px;
  font-style: italic;
}

.admission-form ol {
  margin-left: 20px;
}

.admission-form .branch-section {
  margin:5px 0;
}

.student-details {
  margin: 5px 0;
  position:relative;
}

.language-headers {
  display: flex;
  margin: 5px 0;
  align-items:center;
}

.english-header {
  flex: 1;
  text-align: center;
  font-weight: bold;
}

.hindi-header {
  flex: 1;
  text-align: center;
  font-weight: bold;
}

.photo-area {
  flex: 0 0 100px;
  display: flex;
  justify-content: center;
  position:absolute;
  right:10px;
  top:30px;
}

.admission-form .photo-box {
  width: 120px;
  height: 140px;
  border: 1px solid var(--form-border-color);
  display: flex;
  align-items: center;
  justify-content: center;
  text-align: center;
  font-size: 12px;
  background-color: var(--form-box-color);
}

.name-boxes {
  flex: 1;
  height:24px;    max-width:200px;
}

.optional {
  font-weight: normal;
  font-size: 10px;
  font-style: italic;
}

.small-input {
  flex: 0 0 80px;
  margin-right: 20px;
}

.gender-label {
  flex: 0 0 auto;
  margin: 0 5px;
}

.checkbox {
    flex: 0 0 25px;
    height: 22px;
    margin-right: 10px;
    min-height: 24px;
    background-color: var(--form-box-color);
    border: 1px solid var(--form-border-color);
}
.category-label, .religion-label {
  flex: 0 0 auto;
  margin: 0 5px;
}

.blood-group {
  flex: 0 0 auto;
  margin: 0 10px;
}

.blood-input {
  flex: 0 0 80px;
}

.contact-boxes {
  flex: 1;
  height:22px;
}

.contact-note {
  margin-left: 150px;
  font-size: 12px;
  margin-bottom: 10px;
  font-style: italic;
}

.dob-boxes, .nationality-boxes {
  flex: 0.5;
  height:22px;
}

.email-boxes {
  flex: 1;
  height:22px;
}

.address-boxes {
  flex: 1;
  height: 22px;
}

.address-continuation {
  margin-left: 124px;
}

.signature {
  display: flex;
  justify-content: flex-end;
  margin-top:12px;
}

.signature-line {
  font-weight: bold;
  border-top: 1px solid var(--form-border-color);
  padding-top: 5px;
  width:260px;
  text-align: center;
  text-transform: uppercase;
}

/* Add print button styling */
.print-controls {
  text-align: center;
  margin:12px 0;
}

.print-controls button {
  padding: 10px 20px;
  background-color: #4CAF50;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  font-size: 16px;
  font-weight: bold;
}

.print-controls button:hover {
  background-color: #45a049;
}
    </style>
    <div id="loader" runat="server"></div>
    <div class="vd_content-section clearfix">
        
         <div id="app">
              <div class="print-controls">
                <button onclick="window.print()">Print Form</button>
              </div>
            </div>
    
        <div class="admission-form">
          <div class="header">
            <div class="logo">
              <img src="https://via.placeholder.com/100" alt="Pioneer Montessori School Logo" class="school-logo">
            </div>
            <div class="school-info">
                <div style="display:flex; justify-content:space-between;">
                      <div class="since">Since 1963</div>
                        <div class="pioneer-text">Pioneer in the field of Education</div>
                </div>
              <h1>PIONEER MONTESSORI SCHOOL / INTER COLLEGE</h1>
              <div class="location">LUCKNOW / BARABANKI</div>
              <div class="address">
                H.O. Sec-3, Udyan - 1, Eldeco, Lucknow • Ph : 0522-4331133<br>
                E-mail : pioneer.ho1963@gmail.com • Website : www.pioneermontessori.in
              </div>
            </div>
       
          </div>

          <div class="form-title">
            <h2>ADMISSION FORM</h2>
            <div class="serial-number">Sl. No. <span class="sl-no">2654</span></div>
          </div>

          <div class="office-use">
            <div class="section-title">FOR OFFICE USE ONLY</div>
            <div class="office-fields">
              <div class="field-row">
                <div class="field-label">Authorised Signatory</div>
                <div class="field-input long-input" style="margin-left:4px;"></div>
                <div class="field-label" style="text-align:right; padding-right:4px;">Class Allotted</div>
                <div class="field-input long-input" style="flex: 0 0 194px;"></div>
              </div>
              <div class="field-row">
                <div class="field-label">SR No./Admission No.</div>
                <div class="boxes" style="padding-left:4px;">
                  <div class="box-row">
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                  </div>
                </div>
                <div class="field-label" style="text-align:right; padding-right:4px;">Date of Admission</div>
                <div class="boxes">
                  <div class="box-row">
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>               
                  </div>
                </div>
              </div>
            </div>
          </div>

          <div class="instructions">
            <div class="instruction-title">Instructions :</div>
            <ol>
              <li>All fields are mandatory to fill.</li>
              <li>Fill up the form in bold and CAPITAL letters.</li>
              <li>No cutting or over writing is allowed.</li>
              <li>Fill all the data as per Birth Certificate/Transfer/Aadhar Card.</li>
            </ol>
          </div>

          <div class="branch-section">
            
            <div class="boxes">
                <div class="field-label">BRANCH</div>
              <div class="box-row">
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>
                <div class="box"></div>                         
              </div>
            </div>
          </div>
          
          <div class="student-details">
            <div class="section-title" style="white-space:nowrap;">STUDENT'S DETAILS (As per BC/Aadhar/TC)</div>
            <div class="language-headers">
              <div class="english-header" style="max-width:330px;">English</div>
              <div class="hindi-header" style="max-width: 200px;">Hindi</div>
              <div class="photo-area">
                <div class="photo-box">
                  Latest<br>passport size<br>colored<br>photograph
                </div>
              </div>
            </div>

            <div class="field-row">
              <div class="field-label">FIRST NAME</div>
              <div class="box-row">
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                </div>
              <div class="field-input name-boxes hindi"></div>
            </div>

            <div class="field-row">
              <div class="field-label" style="white-space:nowrap;">MIDDLE NAME <br /><span class="optional">(Optional)</span></div>
              <div class="box-row">
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                </div>
              <div class="field-input name-boxes hindi"></div>
            </div>

            <div class="field-row">
              <div class="field-label">LAST NAME <br /><span class="optional">(Optional)</span></div>
              <div class="box-row">
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
            </div>
              <div class="field-input name-boxes hindi"></div>
            </div>

            <div class="field-row">
              <div class="field-label">ADMISSION IN CLASS</div>
              <div class="field-input small-input" style="max-width:120px !important;"></div>
              <div class="field-label gender-label" style="text-align:right; padding-right:4px;flex: 0 0 34px !important;">GENDER</div>
              <div class="field-label gender-label" style="text-align:right; padding-right:4px;flex: 0 0 34px !important;">M</div><div class="checkbox"></div>
              <div class="field-label gender-label" style="text-align:right; padding-right:4px;flex: 0 0 34px !important;">F</div>
              <div class="checkbox"></div>
            </div>

            <div class="field-row">
              <div class="field-label">CATEGORY</div>
              <div class="field-label category-label" style="flex: 0 0 34px !important;">GEN.</div>
              <div class="checkbox"></div>
              <div class="field-label category-label" style="flex: 0 0 34px !important;">OBC</div>
              <div class="checkbox"></div>
              <div class="field-label category-label" style="flex: 0 0 34px !important;">SC</div>
              <div class="checkbox"></div>
              <div class="field-label category-label" style="flex: 0 0 34px !important;">ST</div>
              <div class="checkbox"></div>
              <div class="field-label blood-group" style="flex: 0 0 34px !important;">BLOOD GROUP</div>
              <div class="checkbox"></div>
            </div>

            <div class="field-row">
              <div class="field-label">AADHAR NO.</div>
              <div class="boxes">
                <div class="box-row">
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                  <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                </div>
              </div>
            </div>

            <div class="field-row">
              <div class="field-label">RELIGION</div>
              <div class="field-label category-label" style="flex: 0 0 34px !important;">HINDU</div>
              <div class="checkbox"></div>
              <div class="field-label category-label" style="flex: 0 0 34px !important;">MUSLIM</div>
              <div class="checkbox"></div>
              <div class="field-label category-label" style="flex: 0 0 34px !important;">SIKH</div>
              <div class="checkbox"></div>
              <div class="field-label category-label" style="flex: 0 0 34px !important;">CHRISTIAN</div>
              <div class="checkbox"></div>
              <div class="field-label category-label" style="flex: 0 0 34px !important;">OTHERS</div>
              <div class="checkbox"></div>
            </div>

            <div class="field-row" style="margin-bottom:0 !important;">
              <div class="field-label">CONTACT<br /> <span class="optional">(Mobile)</span></div>
                <div class="field-label" style="text-align:right; padding-right:4px;max-width:20px;">(1)</div>
              <div class="field-input contact-boxes"></div>
              <div class="field-label" style="text-align:right; padding-right:4px;max-width:50px;">(2)</div>
              <div class="field-input contact-boxes"></div>
            </div>
            <div class="contact-note">(EMERGENCY/PREFERRED FOR SCHOOL SMS)</div>

            <div class="field-row">
              <div class="field-label">DATE OF BIRTH</div>
              <div class="field-input dob-boxes"></div>
              <div class="field-label" style="text-align:right; padding-right:4px;">NATIONALITY</div>
              <div class="field-input nationality-boxes"></div>
            </div>

            <div class="field-row">
              <div class="field-label">EMAIL ID</div>
              <div class="field-input email-boxes"></div>
            </div>

           <!--<div class="page-break"></div>-->

            <div class="field-row">
              <div class="field-label">PERMANENT <br />ADDRESS</div>
              <div class="field-input address-boxes"></div>
            </div>
            <div class="field-row address-continuation">
              <div class="field-input address-boxes"></div>
            </div>
            <div class="field-row address-continuation">
              <div class="field-input address-boxes"></div>
            </div>
            <div class="field-row address-continuation">
              <div class="field-input address-boxes"></div>
            </div>

           <div class="page-break"></div>

            <div class="field-row">
              <div class="field-label">CURRENT <br />ADDRESS</div>
              <div class="field-input address-boxes"></div>
            </div>
            <div class="field-row address-continuation">
              <div class="field-input address-boxes"></div>
            </div>
            <div class="field-row address-continuation">
              <div class="field-input address-boxes"></div>
            </div>
            <div class="field-row address-continuation">
              <div class="field-input address-boxes"></div>
            </div>
          </div>

            <div class="student-details">
              <div class="section-title" style="white-space:nowrap;padding-top:20px;">FATHER'S DETAILS</div>
              <div class="language-headers">
                <div class="english-header" style="max-width:330px;">English</div>
                <div class="hindi-header" style="max-width: 200px;">Hindi</div>
                <div class="photo-area">
                  <div class="photo-box">
                    Latest<br>passport size<br>colored<br>photograph
                  </div>
                </div>
              </div>
            </div>

              <div class="field-row">
                <div class="field-label">FIRST NAME</div>
                <div class="box-row">
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                  </div>
                <div class="field-input name-boxes hindi"></div>
              </div>

              <div class="field-row">
                <div class="field-label" style="white-space:nowrap;">MIDDLE NAME <br /><span class="optional">(Optional)</span></div>
                <div class="box-row">
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                  </div>
                <div class="field-input name-boxes hindi"></div>
              </div>

              <div class="field-row">
                <div class="field-label">LAST NAME <br /><span class="optional">(Optional)</span></div>
                <div class="box-row">
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                </div>
                <div class="field-input name-boxes hindi"></div>
              </div>

              <div class="field-row">
                <div class="field-label">AADHAR NO.</div>
                <div class="boxes">
                  <div class="box-row">
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                    <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                  </div>
                </div>
              </div>

                <div class="field-row">
                  <div class="field-label">QUALIFICATION</div>
                  <div class="boxes">
                    <div class="box-row">
                      <div class="box2"></div>
                      <div class="box2"></div>
                      <div class="box2"></div>
                      <div class="box2"></div>
                      <div class="box2"></div>
                      <div class="box2"></div>
                      <div class="box2"></div>
                      <div class="box2"></div>
                      <div class="field-label" style="text-align:right; padding-right:4px;">DOB</div>
                        <div class="box2"></div>
                        <div class="box2"></div>
                        <div class="box2"></div>
                        <div class="box2"></div>
                        <div class="box2"></div>
                        <div class="box2"></div>
                        <div class="box2"></div>
                        <div class="box2"></div>
                        <div class="box2"></div>
                    </div>
                  </div>
                </div>

               <div class="branch-section">                  
                  <div class="boxes">
                      <div class="field-label">PROFESSION</div>
                    <div class="box-row">
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>                                        
                    </div>
                  </div>
                </div>
             
                <div class="field-row">
                  <div class="field-label">DESIGNATION</div>
                  <div class="boxes">
                    <div class="box-row">
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="field-label" style="text-align:right; padding-right:4px;">ANNUAL INCOME Rs.</div>
                      <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                    </div>
                  </div>
                </div>

                <div class="branch-section">                  
                   <div class="boxes">
                       <div class="field-label">OFFICE ADDRESS</div>
                     <div class="box-row">
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>                   
                     </div>
                   </div>
                 </div>

                <div class="field-row">
                  <div class="field-label">CONTACT NO.<br /> <span class="optional"> (Mobile)</span></div>
                  <div class="boxes">
                      <div class="field-label" style="text-align:right; padding-right:4px; max-width:20px;">1</div>
                    <div class="box-row">
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                      <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>                       
                      <div class="field-label" style="text-align:right; padding-right:4px; max-width:20px;">2</div>
                      <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>                       
                    </div>
                  </div>
                </div>
            
             <div class="student-details">
               <div class="section-title" style="white-space:nowrap;">MOTHER'S DETAILS</div>
               <div class="language-headers">
                 <div class="english-header" style="max-width:330px;">English</div>
                 <div class="hindi-header" style="max-width: 200px;">Hindi</div>
                 <div class="photo-area">
                   <div class="photo-box">
                     Latest<br>passport size<br>colored<br>photograph
                   </div>
                 </div>
               </div>
             </div>
            
               <div class="field-row">
                 <div class="field-label">FIRST NAME</div>
                 <div class="box-row">
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                   </div>
                 <div class="field-input name-boxes hindi"></div>
               </div>

               <div class="field-row">
                 <div class="field-label" style="white-space:nowrap;">MIDDLE NAME <br /><span class="optional">(Optional)</span></div>
                 <div class="box-row">
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                   </div>
                 <div class="field-input name-boxes hindi"></div>
               </div>

               <div class="field-row">
                 <div class="field-label">LAST NAME <br /><span class="optional">(Optional)</span></div>
                 <div class="box-row">
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                 </div>
                 <div class="field-input name-boxes hindi"></div>
               </div>

               <div class="field-row">
                 <div class="field-label">AADHAR NO.</div>
                 <div class="boxes">
                   <div class="box-row">
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                     <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                   </div>
                 </div>
               </div>
            
                 <div class="field-row">
                   <div class="field-label">QUALIFICATION</div>
                   <div class="boxes">
                     <div class="box-row">
                       <div class="box2"></div>
                       <div class="box2"></div>
                       <div class="box2"></div>
                       <div class="box2"></div>
                       <div class="box2"></div>
                       <div class="box2"></div>
                       <div class="box2"></div>
                       <div class="box2"></div>
                       <div class="field-label" style="text-align:right; padding-right:4px;">DOB</div>
                         <div class="box2"></div>
                         <div class="box2"></div>
                         <div class="box2"></div>
                         <div class="box2"></div>
                         <div class="box2"></div>
                         <div class="box2"></div>
                         <div class="box2"></div>
                         <div class="box2"></div>
                         <div class="box2"></div>
                     </div>
                   </div>
                 </div>
            
                <div class="branch-section">                  
                   <div class="boxes">
                       <div class="field-label">PROFESSION</div>
                     <div class="box-row">
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>                                          
                     </div>
                   </div>
                 </div>

                 <div class="field-row">
                   <div class="field-label">DESIGNATION</div>
                   <div class="boxes">
                     <div class="box-row">
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="field-label" style="text-align:right; padding-right:4px;padding-left: 10px;">ANNUAL INCOME Rs.</div>
                       <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                     </div>
                   </div>
                 </div>

                 <div class="branch-section">                  
                    <div class="boxes">
                        <div class="field-label">OFFICE ADDRESS</div>
                      <div class="box-row">
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>                                             
                      </div>
                    </div>
                  </div>

                 <div class="field-row">
                   <div class="field-label">CONTACT NO.<br /> <span class="optional"> (Mobile)</span></div>
                   <div class="boxes">
                       <div class="field-label" style="text-align:right; padding-right:4px; max-width:20px;">1</div>
                     <div class="box-row">
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                       <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>                       
                       <div class="field-label" style="text-align:right; padding-right:4px; max-width:20px;">2</div>
                       <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>
                         <div class="box"></div>                        
                     </div>
                   </div>
                 </div>
            
                <div class="signature">
                   <div class="signature-line">SIGNATURE OF PARENT/GUARDIAN</div>
                 </div>

               <div class="page-break"></div>
                <div class="field-row" style="justify-content:space-between; padding-top:12px;">
                    <div class="field-label"><strong>IF SINGLE PARENT (Tick any one if applicable)</strong></div>
                    <div class="box-row">
                          <div class="box text-center">P</div>
                          <div class="box text-center">M</div>
                          <div class="box text-center">S</div>
                          <div class="box text-center">3</div>
                     </div>
                </div>
                <div class="field-row" style="gap:10px;">                  
                    <div class="box-row" style="gap:10px; align-items:center;">
                          <div class="box text-center"></div>
                          <span>FATHER</span>
                     </div>
                    <div class="box-row" style="gap:10px;align-items:center;">
                          <div class="box text-center"></div>
                          <span>MOTHER</span>
                     </div>
                </div>

                <div class="field-row">
                    <div class="field-label"><strong>DETAILS OF AGENCY/PERSON (IF CHILD IS SPONSORED)</strong></div>                   
                </div>
                 <div class="branch-section">                  
                    <div class="boxes">
                        <div class="field-label">NAME</div>
                      <div class="box-row">
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>                   
                      </div>
                    </div>
                  </div>

                    <div class="branch-section">                  
                       <div class="boxes">
                           <div class="field-label">ADDRESS</div>
                             <div class="box-row">
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>                   
                             </div>
                       </div>
                     </div>

                    <div class="field-row" style="gap:80px;">                  
                       <div class="boxes">
                           <div class="field-label">CONTACT NO.</div>
                             <div class="box-row">
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>                                              
                             </div>
                       </div>

                        <div class="boxes">
                            <div class="box-row">
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>                                            
                            </div>
                        </div>
                 </div>

                <div class="field-row">
                    <div class="field-label"><strong>IN CASE OF STAFF WARD</strong></div>                   
                </div>
                 <div class="branch-section">                  
                    <div class="boxes">
                        <div class="field-label">NAME OF STAFF</div>
                      <div class="box-row">
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>                   
                      </div>
                    </div>
                  </div>

                    <div class="branch-section">                  
                       <div class="boxes">
                           <div class="field-label">BRANCH</div>
                         <div class="box-row">
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>                   
                         </div>
                       </div>
                     </div>

                    <div class="branch-section">                  
                       <div class="boxes">
                           <div class="field-label">DESIGNATION</div>
                             <div class="box-row">
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>                   
                         </div>
                   </div>
                 </div>


                 <div class="field-row" style="padding-block:6px;">
                    <div class="field-label"><strong>DETAILS OF REAL BROTHER/SISTER IN SCHOOL (PMS)</strong></div>                   
                </div>
                 <div class="branch-section">                  
                    <div class="boxes">
                        <div class="field-label">NAME</div>
                      <div class="box-row">
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>
                        <div class="box"></div>   
                          <div class="box"></div>
                          <div class="box"></div>
                      </div>

                        <div class="field-label" style="text-align:right;padding-right:4px;flex: 0 0 64px;">CLASS</div>
                          <div class="box-row">
                            <div class="box"></div>
                            <div class="box"></div>
                            <div class="box"></div>
                            <div class="box"></div>
                        </div>
                    </div>
                  </div>

                    <div class="branch-section">                  
                       <div class="boxes">
                           <div class="field-label">ADMISSION/SR NO.</div>
                         <div class="box-row">
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>                                             
                         </div>

                           <div class="field-label" style="text-align:right;padding-right:4px;flex: 0 0 64px;">BRANCH</div>
                             <div class="box-row">
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                                 <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                 <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                 <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                           </div>
                       </div>
                     </div>
                    <div class="branch-section">                  
                       <div class="boxes">
                           <div class="field-label">NAME</div>
                         <div class="box-row">
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>
                           <div class="box"></div>   
                             <div class="box"></div>
                             <div class="box"></div>
                         </div>

                           <div class="field-label" style="text-align:right;padding-right:4px;flex: 0 0 64px;">CLASS</div>
                             <div class="box-row">
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                               <div class="box"></div>
                           </div>
                       </div>
                     </div>
                      <div class="branch-section">                  
                         <div class="boxes">
                             <div class="field-label">ADMISSION/SR NO.</div>
                           <div class="box-row">
                             <div class="box"></div>
                             <div class="box"></div>
                             <div class="box"></div>
                             <div class="box"></div>
                             <div class="box"></div>                                             
                           </div>

                             <div class="field-label" style="text-align:right;padding-right:4px;flex: 0 0 64px;">BRANCH</div>
                               <div class="box-row">
                                 <div class="box"></div>
                                 <div class="box"></div>
                                 <div class="box"></div>
                                 <div class="box"></div>
                                   <div class="box"></div>
                                  <div class="box"></div>
                                  <div class="box"></div>
                                  <div class="box"></div>
                                   <div class="box"></div>
                                  <div class="box"></div>
                                  <div class="box"></div>
                                  <div class="box"></div>
                                   <div class="box"></div>
                                  <div class="box"></div>
                                  <div class="box"></div>
                                  <div class="box"></div>
                             </div>
                         </div>
                       </div>
                     <div class="field-row">
                        <div class="field-label" style="padding-block:6px;"><strong>DETAILS OF SCHOOL LAST ATTENDED</strong></div>                   
                    </div>
                     <div class="branch-section">                  
                            <div class="boxes">
                                <div class="field-label">NAME OF SCHOOL</div>
                              <div class="box-row">
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>                   
                              </div>
                        </div>
                    </div>

                     <div class="branch-section">                  
                        <div class="boxes">
                            <div class="field-label">YEAR</div>
                            <div class="box-row">
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                            </div>                           
                            <div class="field-label" style="text-align:right;padding-right:4px;flex: 0 0 64px;">CLASS</div>
                            <div class="box-row">
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                            </div>                            
                            <div class="field-label" style="text-align:right;padding-right:4px;flex: 0 0 64px;">MARKS OBTAINED</div>
                            <div class="box-row">
                                 <div class="box"></div>
                                 <div class="box"></div>
                                 <div class="box"></div>
                             </div>
                             <div class="field-label" style="text-align:right;padding-right:4px;flex: 0 0 64px;">BOARD</div>
                             <div class="box-row">
                                <div class="box"></div>
                                <div class="box"></div>
                                <div class="box"></div>
                                 <div class="box"></div>
                            </div>              
                          </div>
                        </div>
                        
                          <div class="field-row">
                             <div class="field-label" style="padding-block:6px;"><strong>SUBJECTS PROPOSED TO OFFER</strong></div>                   
                         </div>
                         <div class="branch-section">                  
                            <div class="boxes">
                                <div class="field-label">SUBJECT (1)</div>
                                <div class="box-row">
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>  
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div> 
                                </div>
                                <div class="field-label" style="text-align:right;padding-right:4px;">SUBJECT (2</div>
                                <div class="box-row">
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>  
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div> 
                                </div>
                            </div>
                          </div>

                          <div class="branch-section">                  
                           <div class="boxes">
                               <div class="field-label">SUBJECT (3)</div>
                               <div class="box-row">
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>  
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div> 
                               </div>
                               <div class="field-label" style="text-align:right;padding-right:4px;">SUBJECT (4)</div>
                               <div class="box-row">
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>  
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div> 
                               </div>
                           </div>
                         </div>

                        <div class="branch-section">                  
                           <div class="boxes">
                               <div class="field-label">SUBJECT (5)</div>
                               <div class="box-row">
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>  
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div> 
                               </div>
                               <div class="field-label" style="text-align:right;padding-right:4px;">SUBJECT (6)</div>
                               <div class="box-row">
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>  
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div> 
                               </div>
                           </div>
                         </div>

                        <div class="branch-section">                  
                           <div class="boxes">
                               <div class="field-label">SUBJECT (7)</div>
                               <div class="box-row">
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>  
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div> 
                               </div>
                               <div class="field-label" style="text-align:right;padding-right:4px;">SUBJECT (8)</div>
                               <div class="box-row">
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>  
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div> 
                               </div>
                           </div>
                         </div>
                        <div class="branch-section">                  
                           <div class="boxes">
                               <div class="field-label">SUBJECT (9) <br /><span class="optional">OPTIONAL</span></div>
                               <div class="box-row">
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>  
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div> 
                               </div>
                               <div class="field-label" style="text-align:right;padding-right:4px;">SUBJECT (10) <br /><span class="optional">OPTIONAL</span></div>
                               <div class="box-row">
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>  
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div>
                                   <div class="box"></div> 
                               </div>
                           </div>
                         </div>

                         <div class="field-row">
                            <div class="field-label" style="padding-block:6px;"><strong>MEDICAL HISTORY OF CHILD</strong> <strong>HEARING</strong></div>                   
                        </div>
                        
                        <div class="boxes" style="padding-bottom:10px;">
                            <div class="field-label" style="flex: 0 0 244px;">ANY DIFFICULTY OBSERVED</div>
                            <div class="box-row" style="padding-left:20px;">
                                <div class="box"></div>
                                <div class="field-label" style="flex: 0 0 34px;padding-left:4px;">YES</div>
                                <div class="box"></div>
                                <div class="field-label" style="flex: 0 0 34px;padding-left:4px;">NO</div>
                            </div>                           
                         </div>
                        <div class="boxes">
                            <div class="field-label" style="flex: 0 0 244px;">ANY CONSULTATION WITH DOCTOR DONE</div>
                            <div class="box-row" style="padding-left:20px;">
                                <div class="box"></div>
                                <div class="field-label" style="flex: 0 0 34px;padding-left:4px;">YES</div>
                                <div class="box"></div>
                                <div class="field-label" style="flex: 0 0 34px;padding-left:4px;">NO</div>
                            </div>                           
                         </div>

                         <div class="branch-section">                  
                            <div class="boxes">
                                <div class="field-label">IF YES, EXPLAIN</div>
                                  <div class="box-row">
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>                   
                              </div>
                            </div>
                        </div>
                        <div class="field-row">
                            <div class="field-label"><strong>VISION</strong></div>                   
                        </div>
                        <div class="boxes" style="padding-bottom:10px;">
                            <div class="field-label" style="flex: 0 0 244px;">ANY CONSULTATION WITH DOCTOR DONE</div>
                            <div class="box-row" style="padding-left:20px;">
                                <div class="box"></div>
                                <div class="field-label" style="flex: 0 0 34px;padding-left:4px;">YES</div>
                                <div class="box"></div>
                                <div class="field-label" style="flex: 0 0 34px;padding-left:4px;">NO</div>
                            </div>                           
                         </div>
                         <div class="boxes" style="padding-bottom:10px;">
                            <div class="field-label" style="flex: 0 0 244px;">USE OF SPECTACLES/CORRECTIVE LENSES</div>
                            <div class="box-row" style="padding-left:20px;">
                                <div class="box"></div>
                                <div class="field-label" style="flex: 0 0 34px;padding-left:4px;">YES</div>
                                <div class="box"></div>
                                <div class="field-label" style="flex: 0 0 34px;padding-left:4px;">NO</div>
                            </div>                           
                         </div>

                        <div class="field-row">
                            <div class="field-label" style="padding-block:6px;">ANY ALLERGY/ANY MEDICAL INFORMATION THAT SCHOOL SHOULD BE AWARE OF</div>                   
                        </div>
                         <div class="branch-section">                  
                            <div class="boxes">                               
                                  <div class="box-row">
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>
                                    <div class="box"></div>                   
                              </div>
                            </div>
                        </div>

                         <div class="signature" style="padding-top:12px;">
                            <div class="signature-line">SIGNATURE OF PARENT/GUARDIAN</div>
                          </div>
                            <div class="page-break"></div>

                         <div class="field-row" style="justify-content:space-between; padding-top:12px;">
                             <div class="field-label"><strong>DOCUMENTS ATTACHED</strong> (All the copies attached should be self attested by guardian/parents)<br />                                 <span class="optional">(tick all the documents attached)</span>
                             </div>
                             <div class="box-row">
                                   <div class="box text-center">P</div>
                                   <div class="box text-center">M</div>
                                   <div class="box text-center">S</div>
                                   <div class="box text-center">3</div>
                              </div>
                         </div>

                         <div class="boxes" style="padding-bottom:10px;padding-top:20px;">                           
                           <div class="box-row">
                               <div class="box"></div>
                               <div class="field-label" style="padding-left:4px;flex: 0 0 180px;"><strong>LAST REPORT CARD</strong><br /><span class="optional">(COPY)</span></div> 
                               <div class="field-label" style="font-weight:normal;">INSTITUTION NAME</div>
                               <div class="box-row">
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                             </div>
                           </div>                           
                        </div>


                         <div class="boxes" style="padding-bottom:10px;">                           
                           <div class="box-row">
                               <div class="box"></div>
                               <div class="field-label" style="padding-left:4px;flex: 0 0 180px;"><strong>TRANSFER CERTIFICATE</strong><br /><span class="optional">(ORIGINAL)</span></div> 
                               <div class="field-label" style="font-weight:normal;">INSTITUTION NAME</div>
                               <div class="box-row">
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                             </div>
                           </div>                           
                        </div>

                         <div class="boxes" style="padding-bottom:10px;">                           
                           <div class="box-row" style="padding-left:20px;">                               
                               <div class="field-label" style="padding-left:4px;flex: 0 0 120px;font-weight:normal;">DOCUMENT NO.</div>                                
                               <div class="box-row">
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>
                                    <div class="box text-center"></div>                                                                   
                             </div>
                             <div class="field-label" style="padding-right:4px; text-align:right;flex: 0 0 114px;font-weight:normal;">ISSUED DATE</div>                                
                                  <div class="box-row">
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>                                     
                                </div>
                           </div>                           
                        </div>

                            <div class="boxes" style="padding-bottom:10px;">                           
                               <div class="box-row">
                                   <div class="box"></div>
                                   <div class="field-label" style="padding-left:4px;flex: 0 0 180px;"><strong>BIRTH CERTIFICATE</strong><br /><span class="optional">(COPY)</span></div> 
                                   <div class="field-label" style="font-weight:normal;">ISSUING AUTHORITY</div>
                                   <div class="box-row">
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                 </div>
                               </div>                           
                            </div>

                             <div class="boxes" style="padding-bottom:10px;">                           
                               <div class="box-row" style="padding-left:20px;">                               
                                   <div class="field-label" style="padding-left:4px;font-weight:normal;">DOCUMENT NO.</div>                                
                                   <div class="box-row">
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>                                                                      
                                 </div>
                                 <div class="field-label" style="padding-right:4px; text-align:right;flex: 0 0 114px;font-weight:normal;">ISSUED DATE</div>                                
                                        <div class="box-row">
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>                                     
                                        </div>
                                </div>                           
                            </div>
                            <div class="boxes" style="padding-bottom:10px;">                           
                                <div class="box-row">
                                    <div class="box"></div>
                                    <div class="field-label" style="padding-left:4px;flex: 0 0 152px;"><strong>STUDENT'S AADHAR CARD</strong><br /><span class="optional">(COPY)</span></div> 
                                    <div class="field-label" style="flex: 0 0 151px; text-align: right;padding-right: 4px;font-weight:normal;">DOCUMENT NO.</div>
                                    <div class="box-row">
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                  </div>
                                </div>                           
                             </div>

                             <div class="boxes" style="padding-bottom:10px;">                           
                                <div class="box-row">
                                    <div class="box"></div>
                                    <div class="field-label" style="padding-left:4px;flex: 0 0 152px;"><strong>FATHER'S AADHAR CARD</strong><br /><span class="optional">(COPY)</span></div> 
                                    <div class="field-label" style="flex: 0 0 153px; text-align: right;padding-right: 4px;font-weight:normal;">DOCUMENT NO.</div>
                                    <div class="box-row">
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                         <div class="box text-center"></div>
                                  </div>
                                </div>                           
                             </div>

                            <div class="boxes" style="padding-bottom:10px;">                           
                               <div class="box-row">
                                   <div class="box"></div>
                                   <div class="field-label" style="padding-left:4px;flex: 0 0 152px;"><strong>MOTHER'S AADHAR CARD</strong><br /><span class="optional">(COPY)</span></div> 
                                   <div class="field-label" style="flex: 0 0 153px; text-align: right;padding-right: 4px;font-weight:normal;">DOCUMENT NO.</div>
                                   <div class="box-row">
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                 </div>
                               </div>                           
                            </div>

                            <div class="boxes" style="padding-bottom:10px;">                           
                               <div class="box-row">
                                   <div class="box"></div>
                                   <div class="field-label" style="padding-left:4px;flex: 0 0 152px;"><strong>MEDICAL CERTIFICATE</strong><br /><span class="optional">(COPY)</span></div> 
                                   <div class="field-label" style="flex: 0 0 153px; text-align: right;padding-right: 4px;font-weight:normal;">DOCUMENT NO.</div>
                                   <div class="box-row">
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                 </div>
                               </div>                           
                            </div>

                            <div class="boxes" style="padding-bottom:10px; text-align:left;">     
                                <div>
                                    <p><strong>DECLARATION BY THE PARENT</strong></p>
                                    <p style="font-size:12px; font-style:italic;">
                                        I HEREBY DECLARE THAT THE INFORMATION FURNISHED BY ME IS CORRECT OF MY KNOWLEDGE & BELIEF.<br />
                                        I SHALL ABIDE BY THE RULES OF THE SCHOOL.
                                    </p>
                                </div>
                            </div>

                            <div class="boxes" style="padding-bottom:10px; justify-content:space-between;">  
                                <div>
                                    <div class="box-row">
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                            <div class="box text-center"></div>
                                    </div>
                                    <p><strong>SPECIMEN SIGNATURE</strong></p>
                                </div>
                                <div class="signature-line">SIGNATURE OF PARENT/GUARDIAN</div>
                            </div>

                            <div class="boxes" style="padding-bottom:10px; justify-content:space-around;">  
                                <div style="text-align:center;">
                                    <div class="box-row">
                                        <div class="box text-center" style="width:160px;"></div>                                           
                                    </div>
                                    <p><strong>FATHER</strong></p>
                                </div>  
                                <div style="text-align:center;">
                                    <div class="box-row">
                                        <div class="box text-center" style="width:160px;"></div>                                           
                                    </div>
                                    <p><strong>MOTHER</strong></p>
                                </div> 
                                <div style="text-align:center;">
                                    <div class="box-row">
                                        <div class="box text-center" style="width:160px;"></div>                                           
                                    </div>
                                    <p><strong>GUARDIAN</strong></p>
                                </div> 
                            </div>
                            <div>
                                <div class="field-label">FOR OFFICE USE</div>    
                                <p>CERTIFIED THAT I HAVE CHECKED THE APPLICATION FORM AND THE RELEVANT PAPERS ARE FOUND IN ORDER.</p>                                
                            </div>
                            <div class="signature" style="padding-top:30px;">
                               <div class="signature-line">SIGNATURE OF ADMISSION INCHARGE</div>
                             </div>
                            <div class="boxes" style="padding-bottom:10px;padding-top:30px;">                           
                               <div class="box-row">                                  
                                   <div class="field-label" style="flex: 0 0 220px;">APPROVED FOR ADMISSIONTO CLASS</div>                                    
                                    <div class="box-row">
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>                                       
                                    </div>
                                    <div class="field-label" style="text-align:right; padding-right:6px;flex: 0 0 54px;">SEC.</div>  
                                   <div class="box-row">
                                        <div class="box text-center"></div>
                                        <div class="box text-center"></div>                                                                           
                                    </div>
                                   <div class="field-label" style="padding-left:6px;">AFTER CHECKING THE RELEVANT PAPERS.</div>
                               </div>                           
                            </div>
                             <div class="field-row" style=" justify-content:space-between;padding-top:30px;">
                                    <div class="field-label">DATE</div> 
                                    <div class="signature-line" style="text-align:right;">SIGNATURE OF PRINCIPAL</div>
                             </div>
                             <div class="boxes" style="padding-bottom:10px; justify-content:space-around;">  
                                 <div>
                                   RECEIPT NO.
                                 </div>                                   
                                 <div>
                                     <div class="box-row">
                                         <div class="box text-center" style="width:160px;"></div>                                           
                                     </div>                                    
                                 </div> 
                                 <div>
                                   AMOUNT RS.
                                </div> 
                                  <div>
                                     <div class="box-row">
                                         <div class="box text-center" style="width:160px;"></div>                                           
                                     </div>                                    
                                 </div> 
                                  <div>
                                       DATE
                                  </div> 
                                  <div>
                                    <div class="box-row">
                                        <div class="box text-center" style="width:160px;"></div>                                           
                                    </div>                                    
                                </div> 
                             </div>

                    </div>
                </div>
           
   

</asp:Content>

