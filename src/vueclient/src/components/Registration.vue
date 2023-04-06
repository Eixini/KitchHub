<template>
    <div>
        <Form @submit="handleRegister" :validation-schema="schema">
            <div>
                <label for="email">E-mail</label>
                <Field name="email" type="text"/>
                <ErrorMessage name="email"/>
            </div>

            <div>
                <label for="nickname">Nickname</label>
                <Field name="nickname" type="text"/>
                <ErrorMessage name="nickname"/>
            </div>

            <div>
                <label for="password">Password</label>
                <Field name="password" type="text"/>
                <ErrorMessage name="password"/>
            </div>

            <div>
                <button>
                    <span v-show="loading"></span>
                    Registration
                </button>
            </div>
        </Form>

        <div v-if="message"> {{ message }}</div>
    </div>
</template>

<script lang="js">
import { Form, Field, ErrorMessage } from "vee-validate";
import * as yup from "yup";

export default {
    name: "Register",
    components: {
        Form,
        Field,
        ErrorMessage
    },

    date() {
        const schema = yup.object().shape({
            email: yup
                .string()
                .required("E-mail is required!")
                .email("E-mail is invalid!")
                .max(40, "Must be maximum 50 characters!"),
            nickname: yup
                .string()
                .required("Nickname is required!")
                .min(3, "Must be at least 3 characters!")
                .max(20, "Must be maximum 20 characters!"),
            password: yup
                .string()
                .required("Password is required!")
                .min(6, "Must be at least 6 characters!")
                .max(40, "Must be maximum 40 characters!"),
            
        });

        return {
            successful: false,
            loading: false,
            message: "",
            schema,
        };
    },

    computed: {
        loggedIn() {
            return this.$store.state.auth.status.loggedIn;
        },
    },
    
    mounted() {
        if (this.loggedIn) {
            this.$router.push("/profile");
        }
    },

    methods: {
    handleRegister(user) {
      this.message = "";
      this.successful = false;
      this.loading = true;

      this.$store.dispatch("auth/register", user).then(
        (data) => {
          this.message = data.message;
          this.successful = true;
          this.loading = false;
        },
        (error) => {
          this.message =
            (error.response &&
              error.response.data &&
              error.response.data.message) ||
            error.message ||
            error.toString();
          this.successful = false;
          this.loading = false;
        }
      );
    },
  },
}

</script>