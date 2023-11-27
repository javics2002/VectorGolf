To test or modify the LevelSelection.uxml, you can use the following UXML code:

```uxml
<ui:GroupBox name="row" class="level-row">
    <ui:Button display-tooltip-when-elided="true" name="level-1" class="level-button">
        <ui:VisualElement name="thumbnail" class="button-icon level-thumbnail" />
        <ui:VisualElement name="flag-bg" class="level-flag-container">
            <ui:VisualElement name="image" class="level-flag-image" />
        </ui:VisualElement>
        <ui:Label tabindex="-1" text="1" display-tooltip-when-elided="true" name="number" class="level-number" />
    </ui:Button>
    <!-- 3 more items... -->
</ui:GroupBox>
```

This will display the elements as they are in the game. You can then modify the UXML code to test your changes.