import { ApiService } from './api.service';
import { LibraryMenuComponent } from './library-menu/library-menu.component';
import { LibraryListComponent } from './library-list/library-list.component';
import { HomePageComponent } from './home-page/home-page.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { TestingComponent } from './testing/testing.component';

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    LibraryListComponent,
    LibraryMenuComponent,
    TestingComponent,
  ],
  imports: [
    HttpClientModule,
    BrowserModule,
    RouterModule.forRoot([
      {
        path: 'libraries',
        component: LibraryListComponent,
      },
      {
        path: 'library/:id',
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
    RouterModule.forChild([
      {
        path: 'libraries',
        component: LibraryListComponent,
      },
      {
        path: 'library/:id',
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
