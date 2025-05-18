import { Component, OnInit, OnDestroy } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-about-us',
  standalone: true,
  imports: [MatIconModule],
  templateUrl: './about-us.component.html',
  styleUrls: ['./about-us.component.scss']
})
export class AboutUsComponent implements OnInit, OnDestroy {
  items = [
    {
      title: 'Your Success is Our Success',
      description: 'We are committed to helping you thrive because we succeed when you do!'
    },
    {
      title: 'Secure & Reliable',
      description: 'Our top priority is ensuring the security and reliability of your online transactions. Our expert team builds robust, secure platforms, providing your customers with a safe and seamless purchasing, booking, or transaction experience.'
    },
    {
      title: 'Expert Support',
      description: 'We provide timely support to ensure your business stays online and optimized.'
    },
    {
      title: 'Innovative Solutions',
      description: 'A smooth, intuitive user experience is key to maximizing conversions and customer satisfaction. Our UX designers and developers collaborate to create transactional websites that are effortless to navigate, making every purchase a breeze.'
    }
  ];

  currentIndex = 0;
  private intervalId: any;
  isAnimating = false;
  visibleItemCount = 3;

  ngOnInit(): void {
    this.setupAutoScroll();
  }

  ngOnDestroy(): void {
    this.clearAutoScroll();
  }

  private setupAutoScroll(): void {
    this.clearAutoScroll();
    this.intervalId = setInterval(() => {
      if (!this.isAnimating) {
        this.next();
      }
    }, 5000);
  }

  private clearAutoScroll(): void {
    if (this.intervalId) {
      clearInterval(this.intervalId);
      this.intervalId = null;
    }
  }

  next(): void {
    if (this.isAnimating) return;
    
    this.isAnimating = true;
    this.clearAutoScroll();
    
    this.currentIndex = (this.currentIndex + 1) % this.items.length;
    
    setTimeout(() => {
      this.isAnimating = false;
      this.setupAutoScroll();
    }, 500);
  }

  prev(): void {
    if (this.isAnimating) return;
    
    this.isAnimating = true;
    this.clearAutoScroll();
    
    this.currentIndex = (this.currentIndex - 1 + this.items.length) % this.items.length;
    
    setTimeout(() => {
      this.isAnimating = false;
      this.setupAutoScroll();
    }, 500);
  }

  isActive(itemIndex: number): boolean {
    return itemIndex === this.currentIndex;
  }

  isAbove(itemIndex: number): boolean {
    const nextIndex = (this.currentIndex + 1) % this.items.length;
    return itemIndex === nextIndex;
  }

  isBelow(itemIndex: number): boolean {
    const prevIndex = (this.currentIndex - 1 + this.items.length) % this.items.length;
    return itemIndex === prevIndex;
  }

  getVisibleItems(): number[] {
    const indices = [];
    for (let i = 0; i < this.visibleItemCount; i++) {
      indices.push((this.currentIndex + i) % this.items.length);
    }
    return indices;
  }
}