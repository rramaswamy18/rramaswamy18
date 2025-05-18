import { AfterViewInit, Component, OnInit } from '@angular/core';
import { OwlOptions } from 'ngx-owl-carousel-o';

@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})

export class HomeComponent implements OnInit , AfterViewInit {
  carouselSlides = [
    {
      image: 'laptop-2.jpg',
      title: 'Grow Your Business with Our Affordable Products and Revenue-Based Fees',
      description: 'The company offers its products to small to medium size businesses and takes a small fee based on revenues.',
      buttonText: 'View Details',
      buttonLink: '/projects/web-development'
    },
    {
      image: 'driving-school.jpg',
      title: 'Streamline Your Training Compliance with Our Hosted Solution',
      description: 'The company has a hosted solution for the vocational training ndustry to help them comply with several compliance requirements.',
      buttonText: 'Learn More',
      buttonLink: '/projects/truck-school'
    },
    {
      image: 'resturant.jpg',
      title: 'Revolutionize your food Business with our innovative delivery software',
      description: 'The company has a food delivery software product that it offers to small businesses wishing to be in the food business without having a kitchen!',
      buttonText: 'About Us...',
      buttonLink: '/projects/restaurant'
    },
    {
      image: 'truck-teacher.jpg',
      title: 'Increase Your Online Presence with Our Digital Marketing Services',
      description: 'Maximize your online reach and boost your business growth with our comprehensive digital marketing services',
      buttonText: 'Explore',
      buttonLink: '/projects/truck-teacher'
    }
  ];

  carouselOptions: OwlOptions = {
    loop: true,
    mouseDrag: true,  
    touchDrag: true, 
    pullDrag: false,
    dots: true,
    navSpeed: 900,
    navText: [
      `<span class="material-icons">arrow_back_ios</span>`,
      `<span class="material-icons">arrow_forward_ios</span>`,
    ],
    nav: true,
    responsive: {
      0: { items: 1 },
      576: { items: 1 },
      768: { items: 1 },
      992: { items: 1 },
      1200: { items: 1 }
    },
    autoplay: true,
    autoplayTimeout: 5000,  // Slightly longer timeout for better viewing
    autoplayHoverPause: true,
    autoHeight: false,  // Fixed height for better transitions
    lazyLoad: true,
    animateIn: 'fadeIn',
    animateOut: 'fadeOut',
    smartSpeed: 800,  // Balanced transition speed
    items: 1,
    fluidSpeed: true,  // Add fluid speed for smoother transitions
    stagePadding: 0,
    margin: 0,
    rewind: false,  // Turn off rewind since we're using loop
  };

  testimonialData = [    
    {
      image: 'laptop.jpg',
      title: 'About Us',
      description: 'We create beautiful and modern websites that are optimized for performance and user experience.',
      points: [
        'Gajaashwa, Inc was incorporated in June 2005.',
        'Gajaashwa, Inc has advisors with experience in various industries.',
        'With over 15 years of experience, Gajaashwa, Inc is committed to delivering high-quality software solutions to its clients',
      ],
      redirectTo: '/about-us'
    },
    {
      image: 'laptop-2.jpg',
      title: 'Products and Services',
      description: 'We provide web development solutions and food delivery website software as our products and services.',
      points: [
        'Web development solutions',
        'Food delivery website software',
        'Other digital products and services',
      ],
      redirectTo: '/'
    },
    {
      image: 'laptop-3.jpg',
      title: 'FAQ(s)',
      description: 'What questions would you like to include in your FAQ section for web development solutions and food delivery website software?',
      points: [
        'What technologies and programming languages do you use to develop websites and web applications?????',
        'Can I customize the food delivery website software to suit my business needs?',
        'Do you provide ongoing support and maintenance for the websites and web applications you develop?',
      ],
      redirectTo: '/'
    },
    {
      image: 'group.jpg',
      title: 'Testimonials',
      description: 'Our customers are satisfied with our web development solutions and food delivery website software. Check out our testimonials!',
      points: [
        'Customer satisfaction',
        'Web development and food delivery software solutions are well-received',
        'Testimonials available for verification',
      ],
      redirectTo: '/'
    }
  ];
  constructor() {}

  ngOnInit() {
    this.preloadImages();
  }

  ngAfterViewInit() {
    setTimeout(() => {
      this.applyCarouselFixes();
    }, 500);
  }

  preloadImages() {
    this.carouselSlides.forEach(slide => {
      const img = new Image();
      img.src = slide.image;
    });
  }

  applyCarouselFixes() {
    const carouselElement = document.querySelector('.owl-carousel');
    if (carouselElement) {
      carouselElement.classList.add('no-flash-carousel');
      carouselElement.addEventListener('translated.owl.carousel', () => {
      });
    }
  }

  handleImageError(event: any) {
    event.target.src = 'assets/images/placeholder.jpg';
  }
}


