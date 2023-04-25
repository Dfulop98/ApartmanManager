import { Directive, ElementRef, Input, OnDestroy, OnInit, Renderer2 } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Directive({
  selector: '[appExactActiveLink]'
})
export class ExactActiveLinkDirective implements OnInit, OnDestroy {
  @Input('appExactActiveLink') activeClassName!: string;
  private routerEventsSubscription!: Subscription;

  constructor(private router: Router, private renderer: Renderer2, private el: ElementRef) {}

  ngOnInit() {
    this.routerEventsSubscription = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.updateExactActiveClass();
      }
    });
  }

  ngOnDestroy() {
    if (this.routerEventsSubscription) {
      this.routerEventsSubscription.unsubscribe();
    }
  }

  private updateExactActiveClass() {
    if (this.router.url === this.router.createUrlTree([this.el.nativeElement.getAttribute('routerLink')]).toString()) {
      this.renderer.addClass(this.el.nativeElement, this.activeClassName);
    } else {
      this.renderer.removeClass(this.el.nativeElement, this.activeClassName);
    }
  }
}
