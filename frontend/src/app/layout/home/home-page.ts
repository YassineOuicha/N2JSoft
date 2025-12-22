import { Component } from "@angular/core";
import { NavbarComponent } from "../../shared/components/navbar/navbar.component";

@Component({
  selector: "app-home-page",
  imports: [NavbarComponent],
  templateUrl: "./home-page.html",
  styleUrl: "./home-page.scss",
})
export class HomePage {}
