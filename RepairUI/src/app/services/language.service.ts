import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { en } from '../i18n/translations/en';
import { de } from '../i18n/translations/de';

export type Language = 'en' | 'de';
export type TranslationObject = typeof en;

@Injectable({
  providedIn: 'root'
})
export class LanguageService {
  private translations: Record<Language, TranslationObject> = {
    en,
    de
  };

  private currentLang = new BehaviorSubject<Language>(
    (localStorage.getItem('language') as Language) || 'en'
  );
  
  currentLang$ = this.currentLang.asObservable();

  constructor() {
    const savedLang = localStorage.getItem('language');
    if (savedLang && (savedLang === 'en' || savedLang === 'de')) {
      this.setLanguage(savedLang);
    }
  }

  setLanguage(lang: Language): void {
    localStorage.setItem('language', lang);
    this.currentLang.next(lang);
  }

  translate(key: string): string {
    const keys = key.split('.');
    let value: any = this.translations[this.currentLang.value];
    
    for (const k of keys) {
      if (value && typeof value === 'object' && k in value) {
        value = value[k];
      } else {
        return key;
      }
    }
    
    return typeof value === 'string' ? value : key;
  }
}