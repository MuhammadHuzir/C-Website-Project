<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="ISB42403_GroupProject.HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!-- SqlDataSource for Slideshow -->
    <asp:SqlDataSource ID="SqlSlideshow" runat="server"
        ConnectionString="<%$ ConnectionStrings:GameShopDB %>"
        SelectCommand="SELECT TOP 5 * FROM Products">
    </asp:SqlDataSource>

    <!-- SqlDataSource for Game Grid -->
    <asp:SqlDataSource ID="sqlGameGrid" runat="server"
        ConnectionString="<%$ ConnectionStrings:GameShopDB %>"
        SelectCommand="SELECT TOP 5 * FROM Products">
    </asp:SqlDataSource>

    <!-- Slideshow for All Products -->
    <div class="slideshow-container">
        <div class="slideshow">
            <asp:Repeater ID="rptSlideshow" runat="server" DataSourceID="SqlSlideshow">
                <ItemTemplate>
                    <div class="slide fade">
                        <asp:Image ID="imgProduct" runat="server" ImageUrl='<%# Eval("Image") %>' AlternateText='<%# Eval("Name") %>' />
                        <div class="slide-text">
                            <h3><%# Eval("Name") %></h3>
                            <p>$<%# Eval("Price", "{0:0.00}") %></p>
                            <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CommandArgument='<%# Eval("ProductID") %>' OnClick="btnAddToCart_Click" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <!-- Navigation Buttons -->
        <a class="prev" onclick="moveSlide(-1)">&#10094;</a>
        <a class="next" onclick="moveSlide(1)">&#10095;</a>
    </div>

    <!-- Grid for All Products -->
    <div class="featured-games">
        <h2>TOP 5 Games</h2>
        <div class="game-grid">
            <asp:Repeater ID="rptGameGrid" runat="server" DataSourceID="sqlGameGrid">
                <ItemTemplate>
                    <div class="game-card">
                        <asp:Image ID="imgProduct" runat="server" ImageUrl='<%# Eval("Image") %>' AlternateText='<%# Eval("Name") %>' />
                        <h3><%# Eval("Name") %></h3>
                        <p>$<%# Eval("Price", "{0:0.00}") %></p>
                        <asp:Button ID="btnAddToCart" runat="server" Text="Add to Cart" CommandArgument='<%# Eval("ProductID") %>' OnClick="btnAddToCart_Click" />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>

    <!-- JavaScript for Slideshow -->
    <script>
        let slideIndex = 0;
        showSlide(slideIndex);

        function moveSlide(n) {
            showSlide(slideIndex += n);
        }

        function showSlide(n) {
            const slides = document.getElementsByClassName("slide");
            if (n >= slides.length) slideIndex = 0;
            if (n < 0) slideIndex = slides.length - 1;
            for (let i = 0; i < slides.length; i++) {
                slides[i].style.display = "none";
            }
            slides[slideIndex].style.display = "block";
        }

        // Auto-advance slideshow every 5 seconds
        setInterval(() => moveSlide(1), 5000);
    </script>
</asp:Content>