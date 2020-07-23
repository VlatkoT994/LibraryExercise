import { ApiService } from './api.service';
import { LibraryMenuComponent } from './library-menu/library-menu.component';
import { LibraryListComponent } from './library-list/library-list.component';
import { HomePageComponent } from './home-page/home-page.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { TestingComponent } from './testing/testing.component';
import { EditPageComponent } from './edit-page/edit-page.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    LibraryListComponent,
    LibraryMenuComponent,
    TestingComponent,
    EditPageComponent,
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    FormsModule,
    RouterModule.forRoot([
      {
        path: 'libraries',
        component: LibraryListComponent,
      },
      {
        path: 'libraries/:id',
        component: LibraryMenuComponent,
      },
      {
        path: 'testing',
        component: TestingComponent,
      },
      {
        path: '',
        component: HomePageComponent,
      },
    ]),
  ],
  providers: [ApiService],
  bootstrap: [AppComponent],
})
export class AppModule {}
