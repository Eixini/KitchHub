<template>
    <div class="content">
        <div class="moder-tabs">
            <button type="button" @click="component='RecipesForModeration'">Рецепты для проверки</button>
            <button type="button" @click="component='ViolationReports'">Сообщения о нарушении</button>
        </div>
        <div class="moder-content">
            <keep-alive>
                <component :is="component"></component>
            </keep-alive>
        </div>
    </div>
</template>

<script lang="js">
import RecipesForModeration from './partial/RecipesForModeration.vue';
import ViolationReports from './partial/ViolationReports.vue';

export default {
    components:{
        RecipesForModeration,
        ViolationReports,
    },
    data(){
        return {
            component: 'RecipesForModeration',
        }
    }
    
}
</script>