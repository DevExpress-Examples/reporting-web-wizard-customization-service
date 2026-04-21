import { Component, signal } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.html',
  styleUrls: ['./nav-menu.css'],
  imports: [RouterLink, RouterLinkActive]
})
export class NavMenu {
  protected readonly isExpanded = signal(false);

  collapse() {
    this.isExpanded.set(false);
  }

  toggle() {
    this.isExpanded.update(value => !value);
  }
}
