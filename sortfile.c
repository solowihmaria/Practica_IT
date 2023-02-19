#include <stdio.h>
#include <stdlib.h>
#include <string.h>

void print_usage() {
    printf("Usage: SortFile [Option] File\n");
    printf("Options:\n");
    printf("  -h  Outputs a short description of the options/ набор функций.\n ");
    printf("  -a  Sorts in ascending order (default)/ прямой порядок сортировки .\n");
    printf("  -d  Sorts in descending order/ обратный порядок сортировки.\n");
}

int compare(const void *a, const void *b) {
    return (*(int*)a - *(int*)b);
}

int main(int argc, char *argv[]) {
    FILE *fp;
    int i, num, num_count = 0;
    int *numbers;
    int ascending = 1;

    // Parse command line arguments
    for (i = 1; i < argc; i++) {
        if (strcmp(argv[i], "-h") == 0) {
            print_usage();
            return 0;
        } else if (strcmp(argv[i], "-a") == 0) {
            ascending = 1;
        } else if (strcmp(argv[i], "-d") == 0) {
            ascending = 0;
        } else {
            break;
        }
    }

    // Check if file argument is provided
    if (i >= argc) {
        printf("Error: File argument is missing.\n");
        print_usage();
        return 1;
    }

    // Open the file for reading
    fp = fopen(argv[i], "r");
    if (fp == NULL) {
        printf("Error: Failed to open file: %s\n", argv[i]);
        return 1;
    }

    // Count the number of integers in the file
    while (fscanf(fp, "%d", &num) == 1) {
        num_count++;
    }

    // Allocate memory for the numbers array
    numbers = (int*) malloc(num_count * sizeof(int));
    if (numbers == NULL) {
        printf("Error: Failed to allocate memory for numbers array.\n");
        return 1;
    }

    // Rewind the file pointer to the beginning of the file
    rewind(fp);

    // Read the integers from the file into the numbers array
    i = 0;
    while (fscanf(fp, "%d", &numbers[i]) == 1) {
        i++;
    }

    // Sort the numbers array using the qsort() function
    qsort(numbers, num_count, sizeof(int), compare);

    // Print the sorted numbers array
    if (!ascending) {
        for (i = num_count - 1; i >= 0; i--) {
            printf("%d\n", numbers[i]);
        }
    } else {
        for (i = 0; i < num_count; i++) {
            printf("%d\n", numbers[i]);
        }
    }

    // Free the memory allocated for the numbers array
    free(numbers);

    // Close the file
    fclose(fp);

    return 0;
}
