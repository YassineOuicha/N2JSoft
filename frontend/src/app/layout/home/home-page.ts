import { Component } from "@angular/core";
import { NavbarComponent } from "../../shared/components/navbar/navbar.component";
import { RouterLink } from "@angular/router";

@Component({
  selector: "app-home-page",
  imports: [NavbarComponent, RouterLink],
  templateUrl: "./home-page.html",
  styleUrl: "./home-page.scss",
})
export class HomePage {}
