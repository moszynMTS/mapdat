import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { MapComponent } from './components/map/map.component';
import { ApiCaller } from './shared/apiCaller/apiCaller';
import { HttpClientModule } from '@angular/common/http';
import { CapitalizeFirstLetterPipe } from '../assets/pipes/capitalize-first-letter.pipe';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatSelectModule } from '@angular/material/select';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { InfoviewComponent } from './components/infoview/infoview.component';
import { MatDialogModule } from '@angular/material/dialog';
import { MatExpansionModule } from '@angular/material/expansion';

@NgModule({
  declarations: [
    AppComponent,
    MapComponent,
    CapitalizeFirstLetterPipe,
    InfoviewComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    FormsModule,
    MatSelectModule,
    MatDialogModule,
    MatExpansionModule
  ],
  providers: [    
    ApiCaller,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
