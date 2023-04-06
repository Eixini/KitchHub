import { createRouter } from 'vue-router'

import Home from '@/components/Home.vue'
import EnteringIngredients from '@/components/EnteringIngredients.vue'
import ShowResultRecipes from '@/components/ShowResultRecipes.vue'
import Profile from '@/components/Profile.vue'
import Registration from '@/components/Registration.vue'
import Authorization from '@/components/Authorization.vue'


const routes = [
    { path: '/', component: Home },
    { path: '/enteringingredients', component: EnteringIngredients },
    { path: '/showresultrecipes', component: ShowResultRecipes },
    { path: '/login', component: Authorization },
    { path: '/register', component: Registration },
    { path: '/profile', component: Profile }
]

export default function(history) {
    return createRouter({
        history,
        routes
    })
}