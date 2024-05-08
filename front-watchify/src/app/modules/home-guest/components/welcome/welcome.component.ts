import { Component, ElementRef, HostListener, ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-welcome',
  templateUrl: './welcome.component.html',
  styleUrls: ['./welcome.component.css']
})
export class WelcomeComponent {

  @ViewChild('dataMenuOpenBtn') dataMenuOpenBtn!: ElementRef;
  @ViewChild('dataMenuCloseBtn') dataMenuCloseBtn!: ElementRef;
  @ViewChild('dataNavbar') dataNavbar!: ElementRef;
  @ViewChild('dataOverlay') dataOverlay!: ElementRef;
  @ViewChild('dataHeader') dataHeader!: ElementRef;
  @ViewChild('dataGoTop') dataGoTop!: ElementRef;
  constructor(public router:Router){}
  toggleNavbar() {
    this.dataNavbar.nativeElement.classList.toggle("active");
    this.dataOverlay.nativeElement.classList.toggle("active");
    document.body.classList.toggle("active");
  }
  @HostListener('window:scroll', ['$event'])
  onWindowScroll() {
    this.toggleHeaderSticky();
    this.toggleGoTop();
  }
  toggleHeaderSticky() {
    window.scrollY >= 10 ? this.dataHeader.nativeElement.classList.add("active") : this.dataHeader.nativeElement.classList.remove("active");
  }

  toggleGoTop() {
    window.scrollY >= 500 ? this.dataGoTop.nativeElement.classList.add("active") : this.dataGoTop.nativeElement.classList.remove("active");
  }
  closeNavbar() {
    this.dataNavbar.nativeElement.classList.remove("active");
    this.dataOverlay.nativeElement.classList.remove("active");
    document.body.classList.remove("active");
  }
  goSignIn() {
this.router.navigate(['accounts']);    }
}
