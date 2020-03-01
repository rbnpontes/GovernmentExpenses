import {
    trigger,
    state,
    style,
    animate,
    transition
} from '@angular/animations';
const LoadingAnimations = [
    trigger('loading', [
        transition(':enter', [
            style({ transform: 'translateY(-100%)'}),
            animate('0.5s', style({ transform : 'translateY(0)'}))
        ]),
        transition(':leave', [
            animate('0.3s cubic-bezier(.96,.96,.7,1)', style({transform: 'translateY(-100%)'}))
        ])
    ])
];
export default LoadingAnimations;