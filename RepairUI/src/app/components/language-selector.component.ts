import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LanguageService, Language } from '../services/language.service';

@Component({
  selector: 'app-language-selector',
  standalone: true,
  imports: [CommonModule],
  template: `
    <div class="relative">
      <select
        [value]="currentLang$ | async"
        (change)="onLanguageChange($event)"
        class="appearance-none bg-transparent border border-gray-300 rounded px-3 py-1 text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
      >
        <option value="en">English</option>
        <option value="de">Deutsch</option>
      </select>
    </div>
  `
})
export class LanguageSelectorComponent {
  currentLang$ = this.languageService.currentLang$;

  constructor(private languageService: LanguageService) {}

  onLanguageChange(event: Event) {
    const select = event.target as HTMLSelectElement;
    this.languageService.setLanguage(select.value as Language);
  }
}