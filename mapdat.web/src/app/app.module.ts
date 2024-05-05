import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { MapComponent } from './components/map/map.component';
import { ApiCaller } from './shared/apiCaller/apiCaller';
import { HttpClientModule } from '@angular/common/http';
import { CapitalizeFirstLetterPipe } from '../assets/pipes/capitalize-first-letter.pipe';

@NgModule({
  declarations: [
    AppComponent,
    MapComponent,
    CapitalizeFirstLetterPipe
  ],
  imports: [
    BrowserModule,
    HttpClientModule
  ],
  providers: [    
    ApiCaller,
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
