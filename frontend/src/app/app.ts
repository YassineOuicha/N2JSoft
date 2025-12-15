import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomePage } from "./layout/home/home-page";

@Component({
  imports: [RouterModule, HomePage],
  selector: "app-root",
  templateUrl: "./app.html",
  styleUrl: "./app.css",
})
export class App {
  protected title = "N2F";
}
