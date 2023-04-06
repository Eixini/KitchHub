<template>
    <div class="recipe-result_content">
        <ul id="recipe_list">
            <li v-for="recipe in resultRecipe" :key="recipe.name">
                <button  class="recipe_item"
                         v-on:click="showDescription(recipe.description)">
                         {{ recipe.name }}
                </button>
            </li>
        </ul>

        <textarea id="description_area" v-model="description" readonly />

    </div>
</template>

<script lang="js">
//import { from } from 'responselike';
import { mapState } from 'vuex';

export default {

    name: 'ShowResultRecipes',

    data() {
        return{
            description: "Recipe Text",
            //recipes: []
        }
    },
    methods:{
        showDescription(description){
            this.description = description;
        }
    },
    computed: {
        ...mapState([
            'resultRecipe',
        ])
    }
}

</script>

<style>

.recipe-result_content {
    display: grid;
    column-count: 2;
    width: 100%;
}

#recipe_list {
    grid-column: 1;
    margin-left:0%;
    width: 40%;
    height: 100%;
    display: flex;
}

#description_area {
    grid-column: 2;
    margin-right: 0%;
    display: flex;
    width: 60%;
    height: 100%;
}

ul {
    list-style-type:  none;
    display: flex;
}

.recipe_item {
    background: #7f8ff4;
    color: #fff;
    border-radius: 5px;
    border: none;
    padding: 12px 36px;
    margin: 5px;
    width: auto;
}

</style>