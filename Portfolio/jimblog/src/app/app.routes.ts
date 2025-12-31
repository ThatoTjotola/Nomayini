import { Routes } from '@angular/router';
import { Blog } from './blog/blog';
import { AboutMe } from './about-me/about-me';
import { Home } from './home/home';

export const routes: Routes = [
    {
        path: '',
        title: 'Home',
        component: Home
    },
    {
        path: 'blog',
        title:'Blog',
        component: Blog,
    },
    {
        path: 'about-me',
        title:'About Me',
        component: AboutMe,
    }
];
