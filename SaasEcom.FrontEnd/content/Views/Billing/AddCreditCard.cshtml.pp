﻿@using System.Configuration
@model $rootnamespace$.Controllers.CreditCardViewModel

@{
    ViewBag.Title = "Add credit card";
    Layout = "~/Views/Shared/_Layout.cshtml";
}

<h3><i class="fa fa-credit-card"></i> Add credit card</h3>
<hr />

<div class="row">
    <div class="col-md-8 col-md-offset-2">
        @using (Html.BeginForm("AddCreditCard", "Billing", FormMethod.Post, new { @class = "form-horizontal", role = "form", id = "card-form" }))
        {
            @Html.EditorFor(m => m.CreditCard)
        }
    </div>
</div>

<input type="hidden" id="stripe-publishable-key" name="stripe-publishable-key" value="@ConfigurationManager.AppSettings["StripeApiPublicKey"]" />

@section Scripts {
    <script type="text/javascript" src="https://js.stripe.com/v2/"></script>
    @Scripts.Render("~/bundles/jqueryval")
    <script src="~/Scripts/saasecom.card.form.js"></script>
}